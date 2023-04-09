using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaymakNetwork;
using System.Linq;

public class ServerNetworkManager : MonoBehaviour
{
    private static ServerNetworkManager _instance;
    public static ServerNetworkManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ServerNetworkManager is null!");

            return _instance;
        }
    }
    public static int enemyCount = 0;
    public static Dictionary<int, EnemyData> enemyList = new Dictionary<int, EnemyData>();
    public static Dictionary<int, GameObject> enemyObjectList = new Dictionary<int, GameObject>();

    public static Dictionary<int, PlayerData> playerList = new Dictionary<int, PlayerData>();
    public static Dictionary<int, GameObject> playerObjectList = new Dictionary<int, GameObject>();

    public Queue<int> IDToSet = new Queue<int>();

    public static Queue<ByteBuffer> playerToUpdate = new Queue<ByteBuffer>();

    public static Queue<ActionData> ActionToUpdate = new Queue<ActionData>();

    public GameObject prefab;

#region Server
    private void Awake()
    {
        _instance = this;
        //StartCoroutine(EnemyDataThread());
    }
    private void Update()
    {
        //Must Manipulate Objects within Main thread!
        while (IDToSet.Count > 0)
        {
            SpawnPlayerWithID();
        }
        while (ActionToUpdate.Count > 0)
        {
            ActionData command = ActionToUpdate.Dequeue();
            int connectionID = command.connectionID;
            PlayerStates action = command.action;

            HandleKeyInput(connectionID, action);
        }

    }

    private void SpawnPlayerWithID()
    {        
        int id = IDToSet.Dequeue();
        Debug.Log("IDToSet Triggered with ID: " + id);

        PlayerData pData = new PlayerData { isConnected = true };
        GameObject player = Instantiate(prefab, transform);

        player.name = "Player: " + id;

        playerList.Add(id, pData);
        playerObjectList.Add(id, player);

        Debug.Log(player.name + " has been added to the game");

        JoinGame(id);
    }


    public void FixedUpdate()
    {
        //Post Update Physics
        if (playerToUpdate.Count > 0)
        {
            UpdatePlayer();
        }     

    }
    IEnumerator EnemyDataThread()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            ServerNetworkSend.SendEnemyData(i, enemyList[i]);
        }
        yield return new WaitForSeconds(0.3f);
    }
    private static void UpdatePlayer()
    {
        Debug.Log("PlayerToUpdate Called");
        ByteBuffer buffer = playerToUpdate.Dequeue();

        int connectionID = buffer.ReadInt32();

        PlayerData playerData = playerList[connectionID];
        GameObject player = playerObjectList[connectionID];


        //Position
        float posX = buffer.ReadSingle();
        float posY = buffer.ReadSingle();
        float posZ = buffer.ReadSingle();
        playerData.Position = new Vector3(posX, posY, posZ);
        Debug.Log("Position Received is: " + playerData.Position);

        //Rotation
        float lookX = buffer.ReadSingle();
        float lookY = buffer.ReadSingle();
        float lookZ = buffer.ReadSingle();
        float lookW = buffer.ReadSingle();
        playerData.Rotation = new Quaternion(lookX, lookY, lookZ, lookW);
        Debug.Log("Rotation Received is: " + playerData.Rotation);

        //Velocity
        float velX = buffer.ReadSingle();
        float velY = buffer.ReadSingle();
        float velZ = buffer.ReadSingle();
        playerData.Velocity = new Vector3(velX, velY, velZ);
        Debug.Log("Velocity Received is: " + playerData.Velocity);


        //Health Amount
        playerData.HealthAmount = buffer.ReadInt32();


        player.transform.position = playerData.Position;
        player.transform.rotation = playerData.Rotation;
        player.GetComponentInChildren<Rigidbody>().velocity = playerData.Velocity;
        player.GetComponentInChildren<HealthSystem>().SetHealth(playerData.HealthAmount);


        if (player.GetComponentInChildren<Rigidbody>().velocity.magnitude < 3)
            player.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;

        Debug.Log("Position ACTUAL is: " + player.transform.position);
        Debug.Log("Rotation ACTUAL is: " + player.transform.rotation);
        Debug.Log("Velocity ACTUAL is: " + player.GetComponentInChildren<Rigidbody>().velocity);


        //Maybe to use SendtoAllBut?
        //ServerNetworkSend.SendPlayerData(connectionID, player);
        buffer.Dispose();
    }

    public void JoinGame(int connectionID)
    {
        ServerNetworkSend.InstantiateNetworkPlayer(connectionID);
    }

    public void CreatePlayer(int connectionID)
    {
        IDToSet.Enqueue(connectionID);
    }

    public static void HandleKeyInput(int connectionID, PlayerStates action)
    {
        if (action == PlayerStates.None) return;
        
        ClientPlayerInput player = playerObjectList[connectionID].GetComponentInChildren<ClientPlayerInput>();
        Debug.Log("Receiving Action: " + action);


        if (action == PlayerStates.Ability)
            player.Ability();

        else if (action == PlayerStates.Aim)
            player.Aim();

        else if (action == PlayerStates.ChamberGun)
            player.ChamberGun();

        else if (action == PlayerStates.CookGrenade)
            player.CookGrenade();

        else if (action == PlayerStates.EndInteract)
            player.EndInteract();

        else if (action == PlayerStates.EquipCannon)
            player.EquipCannon();

        else if (action == PlayerStates.Interact)
            player.Interact();

        else if (action == PlayerStates.Jump)
            player.Jump();

        else if (action == PlayerStates.LooksieHere)
            player.LooksieHere();

        else if (action == PlayerStates.Pause)
            player.Pause();

        else if (action == PlayerStates.ReleaseAim)
            player.ReleaseAim();

        else if (action == PlayerStates.Reload)
            player.Reload();

        else if (action == PlayerStates.Revive)
            player.ReviveSelf();

        else if (action == PlayerStates.ShootGun)
            player.ShootGun();

        else if (action == PlayerStates.SprintFalse)
            player.SprintFalse();

        else if (action == PlayerStates.SprintTrue)
            player.SprintTrue();

        else if (action == PlayerStates.ThrowGrenade)
            player.ThrowGrenade();

        else if (action == PlayerStates.ToggleNade)
            player.ToggleNade();

        else if (action == PlayerStates.ToggleWeapon)
            player.ToggleWeapon();
        
    }

#endregion
}
