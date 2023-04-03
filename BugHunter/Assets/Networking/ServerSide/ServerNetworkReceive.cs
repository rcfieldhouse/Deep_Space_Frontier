﻿
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
	public int connectionID;
	public Vector3 LookDirection;
	public Vector3 Position;
	public Vector3 Velocity;

	public bool isDead;
	public bool isConnected;
	public bool isFiring;

	public float HealthAmount;

	public PlayerAnimations animations;
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
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CPlayerRotation] = Packet_PlayerRotation;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CMessage] = Packet_Message;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CAnimation] = Packet_Animate;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CMoveData] = Packet_MoveData;
		ServerNetworkConfig.socket.PacketId[(int)ClientPackets.CLookData] = Packet_LookData;
	}

	private static void Packet_MoveData(int connectionID, ref byte[] data)
	{
		
		ByteBuffer buffer = new ByteBuffer(data);
		float moveX = buffer.ReadSingle();
		float moveY = buffer.ReadSingle();

        //PlayerData pData = GameManager.instance.playerList[buffer.ReadInt32];

        //GameManager._movementQueue.Enqueue(cmd);

		buffer.Dispose();
	}

	private static void Packet_LookData(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		float moveX = buffer.ReadSingle();
		float moveY = buffer.ReadSingle();

		if ((moveX > 30f) || (moveY > 30.0f))
			return;

		//LookCommand cmd = new LookCommand(new Vector2(moveX, moveY), connectionID);

		//GameManager._lookQueue.Enqueue(cmd);


		buffer.Dispose();

	}

	private static void Packet_Animate(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		//AnimationState anim = (AnimationState)buffer.ReadInt32();
		
		buffer.Dispose();
	}

	private static void Packet_SpawnPlayer(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		string msg = buffer.ReadString();

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
	private static void Packet_PlayerRotation(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		float rotX = buffer.ReadSingle();
		float rotY = buffer.ReadSingle();
		float rotZ = buffer.ReadSingle();


		buffer.Dispose();

		

		//GameManager.playerList[connectionID].LookDirection = new Vector3(rotX, rotY, rotZ);
	}

	private static void Packet_Message(int connectionID, ref byte[] data)
	{
		ByteBuffer buffer = new ByteBuffer(data);
		string message = buffer.ReadString();

		buffer.Dispose();

		ServerNetworkSend.SendMessage(connectionID, message);
	}
}

