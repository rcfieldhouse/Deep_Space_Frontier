
using System;
using System.Collections.Generic;
using System.Text;
using KaymakNetwork;
using UnityEngine;

internal class ServerNetworkReceive
{

	internal static void PacketRouter()
	{
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CPing] = Packet_SpawnPlayer;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CKeyInput] = Packet_KeyInput;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CMessage] = Packet_Message;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CPlayerData] = Packet_PlayerData;
	}

	private static void Packet_PlayerData(int connectionID, ref byte[] data)
	{
		if (!ServerNetworkManager.playerList[connectionID].isConnected)
			return;
		Debug.Log("Packet_PlayerData Called");

		ByteBuffer buffer = new ByteBuffer(data);

		ServerNetworkManager.playerToUpdate.Enqueue(buffer);

		buffer.Dispose();
	}

	private static void Packet_SpawnPlayer(int connectionID, ref byte[] data)
	{
		Debug.Log("Packet_SpawnPlayer Triggered");
		ByteBuffer buffer = new ByteBuffer(data);
		string msg = buffer.ReadString();
		
		Console.WriteLine(msg);
		ServerNetworkManager.instance.CreatePlayer(connectionID);
		buffer.Dispose();
	}

	private static void Packet_KeyInput(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		byte playerAction = buffer.ReadByte();

		buffer.Dispose();
		ServerNetworkManager.ActionToUpdate.Enqueue(new ActionData(connectionID, (PlayerStates)playerAction));
	}

	private static void Packet_Message(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		string message = buffer.ReadString();

		buffer.Dispose();

		ServerNetworkSend.SendMessage(connectionID, message);
	}
}


