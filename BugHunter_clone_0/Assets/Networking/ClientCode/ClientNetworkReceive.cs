using KaymakNetwork;
using UnityEngine;



internal static class ClientNetworkReceive
{
    internal static void PacketRouter()
    {
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = Packet_WelcomeMsg;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = Packet_SpawnPlayer;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiateEnemy] = Packet_SpawnEnemy;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SMessage] = Packet_Message;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerData] = Packet_PlayerData;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SEnemyData] = Packet_EnemyData;
    }

    private static void Packet_WelcomeMsg(ref byte[] data)
    {
		Debug.Log("Packet_WelcomeMsg Called");
		ByteBuffer buffer = new ByteBuffer(data);
		int connectionID = buffer.ReadInt32();
        string msg = buffer.ReadString();
        buffer.Dispose();

        Debug.Log(msg);

		ClientNetworkManager.instance.myConnectionID = connectionID;

		//TODO: Seperate the PING from the actual Spawn of players (for a lobby?)
        ClientNetworkSend.SendPing();
    }
	 
    private static void Packet_SpawnPlayer(ref byte[] data)
    {
		Debug.Log("Packet_Spawn Called");
		ByteBuffer buffer = new ByteBuffer(data);
		int connectionID = buffer.ReadInt32();
        Debug.Log("ConnectionID: "+connectionID);

        InstantiateData iData;

		if (connectionID == ClientNetworkManager.instance.myConnectionID)
        {
			iData = new InstantiateData() { connectionID = connectionID, isMyPlayer = true };			
		}
        else
        {
			iData = new InstantiateData() { connectionID = connectionID, isMyPlayer = false };
		}

		ClientNetworkManager.IDToSet.Enqueue(iData);


		buffer.Dispose();
    }

    private static void Packet_KeyInput(ref byte[] data)
    {
        Debug.Log("Packet_KeyInput Called");
    }

    private static void Packet_Message(ref byte[] data)
    {
        Debug.Log("Packet_Message Called");
    }
    private static void Packet_PlayerData(ref byte[] data)
    {
		Debug.Log("Packet_PlayerData Called");
		ByteBuffer buffer = new ByteBuffer(data);
        ClientNetworkManager.playerToMove.Enqueue(buffer);
		buffer.Dispose();
    }
    private static void Packet_EnemyData(ref byte[] data)
    {
        Debug.Log("Packet_Message Called");
        ByteBuffer buffer = new ByteBuffer(4);

        ClientNetworkManager.enemyToMove.Enqueue(buffer);

        buffer.Dispose();
    }

    private static void Packet_SpawnEnemy(ref byte[] data)
    {
        Debug.Log("Packet_SpawnEnemy Called");
        ByteBuffer buffer = new ByteBuffer(4);

        ClientNetworkManager.enemyToSpawn.Enqueue(buffer);

        buffer.Dispose();
    }
}


