using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaymakNetwork;
using System;

internal class ClientNetworkManager : MonoBehaviour
{
    private static ClientNetworkManager _instance;
    public static ClientNetworkManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ServerNetworkManager is null!");
            return _instance;
        }
    }
    public static Dictionary<int, GameObject> enemyList = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();    

    public static Queue<InstantiateData> IDToSet = new Queue<InstantiateData>();
    public static Queue<ByteBuffer> playerToMove = new Queue<ByteBuffer>();
    public static Queue<ByteBuffer> enemyToMove = new Queue<ByteBuffer>();
    public static Queue<ByteBuffer> enemyToSpawn = new Queue<ByteBuffer>();

    public GameObject[] networkingPrefabs;

    public GameObject networkPrefab;
    public GameObject playerPrefab;

    public int myConnectionID;

    private void Awake()
    {
        _instance = this;
        //GameObject player = Instantiate(prefab, transform);
    }

    private void Update()
    {
        //Must Manipulate Objects within Main thread!
        while (IDToSet.Count > 0)
        {
            SpawnPlayerWithID();
        }
        while (playerToMove.Count > 0)
        {
            Debug.Log("PlayerToMove Triggered");
            ByteBuffer buffer = playerToMove.Dequeue();

            int ConnectionID = buffer.ReadInt32();

            if (!playerList.ContainsKey(ConnectionID))
            {
                buffer.Dispose();
                return;
            }

            GameObject player = playerList[ConnectionID];

            player.transform.position = new Vector3(buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle());
            Debug.Log("Position is: "+player.transform.position);

            //rotation
            player.transform.rotation = new Quaternion(buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle());
            Debug.Log("Rotation is: " + player.transform.position);

            //velocity
            player.GetComponentInChildren<Rigidbody>().velocity = new Vector3(buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle());
            Debug.Log("Velocity is: " + player.transform.position);

            player.GetComponentInChildren<HealthSystem>().SetHealth(buffer.ReadInt32());

            if (player.GetComponentInChildren<Rigidbody>().velocity.magnitude < 3)
                player.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;

            buffer.Dispose();
        }

        while (enemyToMove.Count > 0)
        {
            MoveEnemy(enemyToMove.Dequeue());
        }

        while (enemyToSpawn.Count > 0)
        {
            SpawnEnemy(enemyToSpawn.Dequeue());
        }

    }

    private void SpawnEnemy(ByteBuffer buffer)
    {
        int enemyID = buffer.ReadInt32();
        Debug.Log("Spawn Enemy Called for: " + enemyID);

        EnemyType type = (EnemyType)buffer.ReadInt32();

        GameObject enemy = Instantiate(networkingPrefabs[(int)type]);

        float posX = buffer.ReadSingle();
        float posY = buffer.ReadSingle();
        float posZ = buffer.ReadSingle();
        enemy.transform.position = new Vector3(posX, posY, posZ);

        enemyList.Add(enemyID, enemy);

    }

    private void MoveEnemy(ByteBuffer buffer)
    {
        
        int enemyID = buffer.ReadInt32();
        GameObject enemy = enemyList[enemyID];
        Debug.Log("Move Enemy Called for: " + enemyID);

        float posX = buffer.ReadSingle();
        float posY = buffer.ReadSingle();
        float posZ = buffer.ReadSingle();
        enemy.transform.position = new Vector3(posX, posY, posZ);

        float rotX = buffer.ReadSingle();
        float rotY = buffer.ReadSingle();
        float rotZ = buffer.ReadSingle();
        float rotW = buffer.ReadSingle();
        enemy.transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);

        float walkpointX = buffer.ReadSingle();
        float walkpointY = buffer.ReadSingle();
        float walkpointZ = buffer.ReadSingle();
        enemy.GetComponent<AI>().WalkPoint = new Vector3(walkpointX, walkpointY, walkpointZ);

        enemy.GetComponentInChildren<HealthSystem>().currentHealth = buffer.ReadInt32();

        //bool isDead = buffer.ReadBoolean();

        //if (isDead)
        //{
        //    Debug.Log("Killing Enemy: " + enemyID);
        //    enemy.GetComponent<AI>().HandleObjectDeath(enemy.transform);
        //    enemyList.Remove(enemyID);
        //}

        buffer.Dispose();
    }

    private void SpawnPlayerWithID()
    {
        Debug.Log("IDToSet Triggered");

        InstantiateData data = IDToSet.Dequeue();
        GameObject player;

        if (data.isMyPlayer)
        {
            player = Instantiate(playerPrefab, transform);
        }
        else
        {
            player = Instantiate(networkPrefab, transform);
        }

        player.name = "Player: " + data.connectionID;

        playerList.Add(data.connectionID, player);
        Debug.Log(player.name + " has been added to the game");
    }


}