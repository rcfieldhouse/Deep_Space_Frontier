using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaymakNetwork;

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
    public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    
    public static Queue<InstantiateData> _IDToSet = new Queue<InstantiateData>();

    public static Queue<ByteBuffer> _playerToMove = new Queue<ByteBuffer>();

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
        while (_IDToSet.Count > 0)
        {
            Debug.Log("IDToSet Triggered");

            InstantiateData data = _IDToSet.Dequeue();
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
        while (_playerToMove.Count > 0)
        {
            Debug.Log("PlayerToMove Triggered");
            ByteBuffer buffer = _playerToMove.Dequeue();

            GameObject player = ClientNetworkManager.playerList[buffer.ReadInt32()];

            //position
            player.transform.position = new Vector3(buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle());

            //rotation
            player.transform.rotation = Quaternion.Euler(buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle());


            //velocity
            player.GetComponentInChildren<Rigidbody>().velocity = new Vector3(buffer.ReadSingle(), buffer.ReadSingle(), buffer.ReadSingle());

            //Animations
            //player.animations = (PlayerAnimations)buffer.ReadInt32();


            //Health
            // player.HealthAmount = buffer.ReadSingle();

            //TODO: Clientside Reconciliation
            buffer.Dispose();
        }
    }



}