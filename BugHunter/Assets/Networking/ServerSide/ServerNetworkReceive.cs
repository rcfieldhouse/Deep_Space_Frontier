
using System;
using System.Collections.Generic;
using System.Text;
using KaymakNetwork;
using UnityEngine;


#region NetworkCommands
public enum PlayerAnimations
    {	None,
		Idle,
		Dead,
		ForwardDive,
		Backflip,
		BeginJump,
		EndJump,
		PlantTurret,
		EquipWeapon,
		MovingGrenadeCook,
		MovingGrenadeThrow,
		StandingNadeCook,
		StandingNadeThrow,
		RifleIdle,
		RifleRight,
		RifleRunBackward,
		RifleLeft,
		RifleRunForward,
		RifleWalkForward,
		PistolIdle,
		PistolWalk,
		PistolForward,
		PistolLeft,
		PistolRight,
		PistolBack,
		PistolJump,
		RifleReload,
    }
public enum EnemyStates
{
	None,
	Attacking,
	Seeking,
	Patroling,
}
public struct PlayerData
{
	//for use by the server
	public int connectionID;
	public bool isConnected;

	public Vector3 Position;
	public Vector3 LookDirection;	
	public Vector3 Velocity;

	public PlayerAnimations animations;

	public bool isDead;
	public bool isFiring;

	public float HealthAmount;


}
public struct EnemyData
{
	public Vector3 LookDirection;
	public Vector3 Position;

	public EnemyStates Seeking;
	public bool isDead;
}
#endregion

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
		//Data Packing Must be Done in this order to be valid
		if (!ServerNetworkManager.playerList[connectionID].isConnected)
			return;

		ByteBuffer buffer = new ByteBuffer(data);
		PlayerData tempData = new PlayerData();

		//position
		float posX = buffer.ReadSingle();
		float posY = buffer.ReadSingle();
		float posZ = buffer.ReadSingle();
		tempData.Position = new Vector3(posX, posY, posZ);

		//Rotation
		float lookX = buffer.ReadSingle();
		float lookY = buffer.ReadSingle();
		float lookZ = buffer.ReadSingle();
		tempData.LookDirection = new Vector3(lookX, lookY, lookZ);

		//Velocity
		float velX = buffer.ReadSingle();
		float velY = buffer.ReadSingle();
		float velZ = buffer.ReadSingle();
		tempData.Velocity = new Vector3(velX, velY, velZ);

		//Animation State
		tempData.animations = (PlayerAnimations)buffer.ReadInt32(); ;

		//Health Amount
		tempData.HealthAmount = buffer.ReadSingle();

		//Bools
		tempData.isDead = buffer.ReadBoolean();
		tempData.isFiring = buffer.ReadBoolean();

		ServerNetworkManager.playerList[connectionID] = tempData;
		ServerNetworkManager.playerToUpdate.Enqueue(connectionID);

		buffer.Dispose();
	}

	private static void Packet_SpawnPlayer(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		string msg = buffer.ReadString();
		Debug.Log("Boop");
		Console.WriteLine(msg);
		ServerNetworkManager.instance.CreatePlayer(connectionID);
		buffer.Dispose();
	}

	private static void Packet_KeyInput(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		byte key = buffer.ReadByte();

		buffer.Dispose();
		ServerInputManager.HandleKeyInput(connectionID, (ServerInputManager.Keys)key);
	}

	private static void Packet_Message(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		string message = buffer.ReadString();

		buffer.Dispose();

		ServerNetworkSend.SendMessage(connectionID, message);
	}
}


