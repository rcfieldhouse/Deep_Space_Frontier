using KaymakNetwork;
using UnityEngine;

#region NetworkCommands
public enum PlayerAnimations
{
	None,
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
	PlayerData(int id)
	{
		connectionID = id;
		isConnected = true;

		Position = new Vector3(0, 0, 0);
		LookDirection = new Vector3(0, 0, 0);
		Velocity = new Vector3(0, 0, 0);

		animations = 0;

		isDead = false;
		isFiring = false;

		HealthAmount = 200;
	}
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
public struct InstantiateData
{
	public int connectionID;
	public bool isMyPlayer;
}
#endregion

internal static class ClientNetworkReceive
{
    internal static void PacketRouter()
    {
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = Packet_WelcomeMsg;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = Packet_Spawn;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SMessage] = Packet_Message;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerData] = Packet_PlayerData;

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
	 
    private static void Packet_Spawn(ref byte[] data)
    {
		Debug.Log("Packet_Spawn Called");
		ByteBuffer buffer = new ByteBuffer(data);
		int connectionID = buffer.ReadInt32();

		InstantiateData iData;

		if (connectionID == ClientNetworkManager.instance.myConnectionID)
        {
			iData = new InstantiateData() { connectionID = connectionID, isMyPlayer = true };			
		}
        else
        {
			iData = new InstantiateData() { connectionID = connectionID, isMyPlayer = false };
		}

		ClientNetworkManager._IDToSet.Enqueue(iData);


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

		//Might need to be object.locked because this is multi-threaded data with inconsistent ownership
        ClientNetworkManager._playerToMove.Enqueue(buffer);
		buffer.Dispose();
    }
}


