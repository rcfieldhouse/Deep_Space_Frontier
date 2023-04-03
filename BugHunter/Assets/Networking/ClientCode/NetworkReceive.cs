using KaymakNetwork;
using UnityEngine;

internal static class ClientNetworkReceive
{
    internal static void PacketRouter()
    {
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = Packet_WelcomeMsg;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = Packet_Spawn;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerMove] = Packet_KeyInput;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerRotation] = Packet_PlayerRotation;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SMessage] = Packet_Message;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SAnimation] = Packet_Animate;
    }

    private static void Packet_WelcomeMsg(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        string msg = buffer.ReadString();
        buffer.Dispose();

        Debug.Log(msg);

        ClientNetworkSend.SendPing();
    }

    private static void Packet_PlayerRotation(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        float moveX = buffer.ReadSingle();
        float moveY = buffer.ReadSingle();
        float moveZ = buffer.ReadSingle();
        float moveW = buffer.ReadSingle();

        buffer.Dispose();
    }

    private static void Packet_Spawn(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        //Spawn OTHER players here

        int id = buffer.ReadInt32();
        //GameManager.instance._IDToSet.Enqueue(id);

        Debug.Log("Spawn");

        buffer.Dispose();
    }

    private static void Packet_KeyInput(ref byte[] data)
    {
        Debug.Log("Key Input");
    }

    private static void Packet_Animate(ref byte[] data)
    {
        Debug.Log("Animate");
    }

    private static void Packet_Message(ref byte[] data)
    {
        Debug.Log("Message");

        //ByteBuffer buffer = new ByteBuffer(data);
        //string msg = buffer.ReadString();
        //buffer.Dispose();
        //
        //Debug.Log(msg);
        //
        //NetworkSend.SendPing();
    }
}


