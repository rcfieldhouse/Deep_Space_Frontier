using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Dictionary<int, PlayerData> playerList = new Dictionary<int, PlayerData>();
    public static Dictionary<int, GameObject> playerObjectList = new Dictionary<int, GameObject>();

    public Queue<int> _IDToSet = new Queue<int>();

    public static Queue<int> playerToUpdate = new Queue<int>();

    public GameObject prefab;

    #region Server
    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        //Must Manipulate Objects within Main thread!
        while (_IDToSet.Count > 0)
        {
            int id = _IDToSet.Dequeue();

            PlayerData pData = new PlayerData();
            GameObject player = Instantiate(prefab, transform);
            player.name = "Player: " + id;

            playerList.Add(id, pData);
            playerObjectList.Add(id, player);

            Debug.Log(player.name + " has been added to the game");

            JoinGame(id);
        }
    }

    public void FixedUpdate()
    {
        //Post Update Physics
        if (playerToUpdate.Count > 0)
        {
            UpdatePlayer(playerToUpdate.Dequeue());
        }
    }

    public void JoinGame(int connectionID)
    {
        ServerNetworkSend.InstantiateNetworkPlayer(connectionID);
    }

    public void CreatePlayer(int connectionID)
    {
        _IDToSet.Enqueue(connectionID);
    }

    public void UpdatePlayer(int index)
    {
        PlayerData Data = playerList[index];
        GameObject player = playerObjectList[index];

        player.transform.position = Data.Position;
        player.transform.rotation = Quaternion.Euler(Data.LookDirection);
        player.GetComponent<Rigidbody>().velocity = Data.Velocity;

        //TODO: Need a function that Handles Animations
        //player.AnimationRouter(Data.animations)

        //TODO: Must Call isDead Remotely
        //player.GetComponent<>();

        //TODO: Must Call isFiring Remotely
        //player.GetComponent<>();


    }
    #endregion
}
