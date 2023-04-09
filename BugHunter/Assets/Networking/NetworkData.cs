using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region NetworkCommands

public struct ActionData
{
    public ActionData(int a, PlayerStates b)
    {
		connectionID = a;
		action = b;
    }
	public int connectionID;
	public PlayerStates action;
}
public struct PlayerData
{
	//for use by the server
	public int connectionID;
	public bool isConnected;

	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Velocity;

	public bool isDead;

	public int HealthAmount;
}
public struct EnemyData
{
	public Vector3 Rotation;
	public Vector3 Position;

	public EnemyStates Seeking;
	public int HealthAmount;
	public bool isDead;
}
public struct InstantiateData
{
	public int connectionID;
	public bool isMyPlayer;
}

enum ClientPackets
{
	CPing = 1,
	CKeyInput,
	CMessage,
	CPlayerData,
	CEnemyData,
}
enum ServerPackets
{
	SWelcomeMsg = 1,
	SInstantiatePlayer,
	SInstantiateEnemy,
	SMessage,
	SPlayerData,
	SEnemyData,
}

public enum EnemyStates
{
	None,
	Attacking,
	Seeking,
	Patroling,
}

public enum PlayerStates
{
	None,
	Revive,
	EquipCannon,
	ToggleNade,
	ToggleWeapon,
	//fuck ryan function names
	LooksieHere,
	Pause,
	ShootGun,
	Aim,
	ReleaseAim,
	ChamberGun,
	Reload,
	Jump,
	Interact,
	EndInteract,
	CookGrenade,
	ThrowGrenade,
	Ability,
	SprintTrue,
	SprintFalse,

	//TODO: Delete these for the final build
	GiveUp,
	GoInvincible,
	GoBoss,
}
#endregion

