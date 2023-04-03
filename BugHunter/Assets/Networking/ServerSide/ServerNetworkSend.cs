using System;
using KaymakNetwork;
using UnityEngine;


static class ServerNetworkSend
{
    public static void WelcomeMsg(int connectionID, string msg)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SWelcomeMsg);
        buffer.WriteInt32(connectionID);
        buffer.WriteString(msg);

        ServerNetworkConfig.socket.SendDataTo(connectionID, buffer.Data, buffer.Head);

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
        for (int i = 0; i < ServerNetworkManager.playerList.Count; i++)
            if (ServerNetworkManager.playerList[i].Equals(default(PlayerData)))
                if (i != connectionID)
                    ServerNetworkConfig.socket.SendDataTo(connectionID, PlayerData(connectionID).Data, PlayerData(connectionID).Head);

        ServerNetworkConfig.socket.SendDataToAll(PlayerData(connectionID).Data, PlayerData(connectionID).Head);
    }

    public static void SendMessage(int connectionID, string message)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SMessage);
        buffer.WriteInt32(connectionID);

        string newMessage = "Player " + connectionID + ": " + message + "\n";

        buffer.WriteString(newMessage);
        Debug.Log(newMessage);
        ServerNetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);
        buffer.Dispose();
    }

    internal static void SendPlayerData(int connectionID, PlayerData Data)
    {
        ByteBuffer buffer = new ByteBuffer(4);

        //position
        buffer.WriteSingle(Data.Position.x);
        buffer.WriteSingle(Data.Position.y);
        buffer.WriteSingle(Data.Position.z);

        //rotation
        buffer.WriteSingle(Data.LookDirection.x);
        buffer.WriteSingle(Data.LookDirection.y);
        buffer.WriteSingle(Data.LookDirection.z);

        //velocity
        buffer.WriteSingle(Data.Velocity.x);
        buffer.WriteSingle(Data.Velocity.y);
        buffer.WriteSingle(Data.Velocity.z);

        //Animations

        //bools

        //Health

    }

}


