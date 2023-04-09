using System;
using KaymakNetwork;
using UnityEngine;


static class ServerNetworkSend
{
    public static void WelcomeMsg(int connectionID, string msg)
    {
        Debug.Log("Welcome Message Triggered");
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SWelcomeMsg);
        buffer.WriteInt32(connectionID);
        buffer.WriteString(msg);

        ServerNetworkConfig.socket.SendDataTo(connectionID, buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    public static void InstantiateNetworkPlayer(int connectionID)
    {
        Debug.Log("InstantiateNetworkPlayer Triggered");
        ByteBuffer buffer = new ByteBuffer(4);

        buffer.WriteInt32((int)ServerPackets.SInstantiatePlayer);
        buffer.WriteInt32(connectionID);

        PlayerData data = ServerNetworkManager.playerList[connectionID];
        //Client Responsible for Self-Instantiation
        //Sends update to each other client
        for (int i = 1; i <= ServerNetworkManager.playerList.Count; i++)
           if (!ServerNetworkManager.playerList[i].Equals(default(PlayerData)))
                if (i != connectionID)
                {
                    Debug.Log("Executed @" + i);
                    ServerNetworkConfig.socket.SendDataTo(i, SpawnedPlayer(connectionID).Data, SpawnedPlayer(connectionID).Head);
                }
        
        ServerNetworkConfig.socket.SendDataToAll(SpawnedPlayer(connectionID).Data, SpawnedPlayer(connectionID).Head);
    }

    public static void InstantiateNetworkEnemy(int enemyID, EnemyType type)
    {
        Debug.Log("InstantiateNetworkEnemy Triggered");
        ByteBuffer buffer = new ByteBuffer(4);

        buffer.WriteInt32((int)ServerPackets.SInstantiateEnemy);
        buffer.WriteInt32(enemyID);

        GameObject enemy = ServerNetworkManager.enemyObjectList[enemyID];

        buffer.WriteInt32((int)type);

        buffer.WriteSingle(enemy.transform.position.x);
        buffer.WriteSingle(enemy.transform.position.y);
        buffer.WriteSingle(enemy.transform.position.z);


        ServerNetworkConfig.socket.SendDataToAll(SpawnedEnemy(enemyID).Data, SpawnedEnemy(enemyID).Head);
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

    private static ByteBuffer SpawnedPlayer(int connectionID)
    {
        ByteBuffer buffer = new ByteBuffer(4);

        buffer.WriteInt32((int)ServerPackets.SInstantiatePlayer);
        buffer.WriteInt32(connectionID);

        return buffer;   
    }
    private static ByteBuffer SpawnedEnemy (int enemyID)
    {
        ByteBuffer buffer = new ByteBuffer(4);

        buffer.WriteInt32((int)ServerPackets.SInstantiateEnemy);
        buffer.WriteInt32(enemyID);

        return buffer;

    }

    public static void SendPlayerData(int connectionID, PlayerData Data)
    {
        Debug.Log("SendPlayerData Triggered");
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SPlayerData);

        //ID to send to
        buffer.WriteInt32(connectionID);

        //position
        buffer.WriteSingle(Data.Position.x);
        buffer.WriteSingle(Data.Position.y);
        buffer.WriteSingle(Data.Position.z);

        //rotation
        buffer.WriteSingle(Data.Rotation.x);
        buffer.WriteSingle(Data.Rotation.y);
        buffer.WriteSingle(Data.Rotation.z);
        buffer.WriteSingle(Data.Rotation.w);

        //velocity
        buffer.WriteSingle(Data.Velocity.x);
        buffer.WriteSingle(Data.Velocity.y);
        buffer.WriteSingle(Data.Velocity.z);

        //Health
        buffer.WriteSingle(Data.HealthAmount);

        ServerNetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
    public static void SendEnemyData(int enemyID, EnemyData Data)
    {
        Debug.Log("SendEnemyData Triggered");
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SEnemyData);

        //ID to send to
        buffer.WriteInt32(enemyID);

        //position
        buffer.WriteSingle(Data.Position.x);
        buffer.WriteSingle(Data.Position.y);
        buffer.WriteSingle(Data.Position.z);

        //rotation
        buffer.WriteSingle(Data.Rotation.x);
        buffer.WriteSingle(Data.Rotation.y);
        buffer.WriteSingle(Data.Rotation.z);

        //Walk Point
        buffer.WriteSingle(Data.Position.x);
        buffer.WriteSingle(Data.Position.y);
        buffer.WriteSingle(Data.Position.z);

        //Health
        buffer.WriteSingle(Data.HealthAmount);

        ServerNetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    public static void SendEnemyData(int enemyID, EnemyData Data, Vector3 Walkpoint)
    {
        Debug.Log("SendEnemyData Triggered");
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SEnemyData);

        //ID to send to
        buffer.WriteInt32(enemyID);

        //position
        buffer.WriteSingle(Data.Position.x);
        buffer.WriteSingle(Data.Position.y);
        buffer.WriteSingle(Data.Position.z);

        //rotation
        buffer.WriteSingle(Data.Rotation.x);
        buffer.WriteSingle(Data.Rotation.y);
        buffer.WriteSingle(Data.Rotation.z);

        //Walk Point
        buffer.WriteSingle(Walkpoint.x);
        buffer.WriteSingle(Walkpoint.y);
        buffer.WriteSingle(Walkpoint.z);

        //Health
        buffer.WriteInt32(Data.HealthAmount);



        ServerNetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

}


