using System;
using System.Collections.Generic;
using KaymakNetwork;
using UnityEngine;

enum ServerPackets
{
    SWelcomeMsg = 1,
    SInstantiatePlayer,
    SPlayerMove,
    SPlayerRotation,
    SMessage,
}
public struct MovementOrder
{
    public MovementOrder(Vector3 a, GameObject b)
    {
        movement = a;
        obj = b;
    }
    public Vector3 movement;
    public GameObject obj;
}

internal static class NetworkReceive
{
    internal static void PacketRouter()
    {
        NetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = new KaymakNetwork.Network.Client.DataArgs(Packet_WelcomeMsg);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = new KaymakNetwork.Network.Client.DataArgs(Packet_InstantiateNetworkPlayer);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerMove] = new KaymakNetwork.Network.Client.DataArgs(Packet_PlayerMove);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerRotation] = new KaymakNetwork.Network.Client.DataArgs(Packet_PlayerRotation);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SMessage] = new KaymakNetwork.Network.Client.DataArgs(Packet_Message);
    }
    private static void Packet_WelcomeMsg(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        string msg = buffer.ReadString();
        buffer.Dispose();

        NetworkManager.instance.myConnectionID = connectionID;

        NetworkSend.SendPing();
    }
    private static void Packet_InstantiateNetworkPlayer(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();

        if (connectionID == NetworkManager.instance.myConnectionID)
            NetworkManager.instance.InstantiateNetworkPlayer(connectionID, true);
        else
            NetworkManager.instance.InstantiateNetworkPlayer(connectionID, false);
    }
    private static void Packet_PlayerMove(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        float x = buffer.ReadSingle();
        float y = buffer.ReadSingle();
        float z = buffer.ReadSingle();

        buffer.Dispose();

        if (!GameManager.instance.playerList.ContainsKey(connectionID)) return;

        GameObject player = GameManager.instance.playerList[connectionID];
        Vector3 movement = new Vector3(x, y, z);

        //need to make a custom data structure to pass this into Unity Main Thread
        NetworkManager.instance._ObjectToMove.Enqueue(new MovementOrder(movement,player));

        //Debug.Log(new Vector3(x, y, z));
    }

    private static void Packet_PlayerRotation(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();

        float rotation = GameManager.instance.WrapEulerAngles(buffer.ReadSingle());

        if (connectionID == NetworkManager.instance.myConnectionID) return;

        if (!GameManager.instance.playerList.ContainsKey(connectionID)) return;
        GameManager.instance.playerList[connectionID].transform.rotation = new Quaternion(0, rotation, 0, 0);

        buffer.Dispose();

    }

    private static void Packet_Message(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();

        string message = buffer.ReadString();
        //message += "/n";
        Debug.Log(message);

        buffer.Dispose();

        GameManager.messages.Enqueue(message);
    }

}

