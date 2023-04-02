using System;
using KaymakNetwork;
using UnityEngine;


enum ServerPackets
{
    SWelcomeMsg = 1,
    SInstantiatePlayer,
    SPlayerMove,
    SPlayerRotation,
    SMessage,
    SAnimation,
}

static class NetworkSend
{
    public static void WelcomeMsg(int connectionID, string msg)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SWelcomeMsg);
        buffer.WriteInt32(connectionID);
        buffer.WriteString(msg);

        NetworkConfig.socket.SendDataTo(connectionID, buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    private static ByteBuffer PlayerData(int connectionID)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SInstantiatePlayer);
        buffer.WriteInt32(connectionID);

        //can't access connectionID normally must send twice
        buffer.WriteInt32(connectionID);

        return buffer;
    }

    public static void InstantiateNetworkPlayer(int connectionID)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SInstantiatePlayer);
        buffer.WriteInt32(connectionID);

        //Client Responsible for Self-Instantiation
        //Sends update to each other client
        for (int i = 0; i < GameManager.playerList.Count; i++)
            if (GameManager.playerList[i] != null)
                if (i != connectionID)
                    NetworkConfig.socket.SendDataTo(connectionID, PlayerData(connectionID).Data, PlayerData(connectionID).Head);

        NetworkConfig.socket.SendDataToAll(PlayerData(connectionID).Data, PlayerData(connectionID).Head);
    }

    public static void SendPlayerMove(int connectionID, Vector3 position)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SPlayerMove);
        buffer.WriteInt32(connectionID);

        buffer.WriteSingle(position.x);
        buffer.WriteSingle(position.y);
        buffer.WriteSingle(position.z);

        //Console.WriteLine("X: " + x + " Y: " + y + " Z: " + z);
        NetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    public static void SendPlayerRotation(int connectionID, Quaternion rotation)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SPlayerRotation);
        buffer.WriteInt32(connectionID);

        buffer.WriteSingle(rotation.x);
        buffer.WriteSingle(rotation.y);
        buffer.WriteSingle(rotation.z);
        buffer.WriteSingle(rotation.w);

        buffer.Dispose();
    }

    public static void SendMessage(int connectionID, string message)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SMessage);
        buffer.WriteInt32(connectionID);

        string newMessage = "Player " + connectionID + ": " + message + "\n";

        buffer.WriteString(newMessage);
        Debug.Log(newMessage);
        NetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);
        buffer.Dispose();
    }

    internal static void SendAnimationData(int connectionID)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SAnimation);
        buffer.WriteInt32(connectionID);

        //buffer.WriteInt32((int)animation);
        NetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
}

