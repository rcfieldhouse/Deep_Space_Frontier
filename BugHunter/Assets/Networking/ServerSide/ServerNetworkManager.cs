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
    public Queue<int> _IDToSet = new Queue<int>();

    public Queue<PlayerData> playerDataToChange = new Queue<PlayerData>();

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

            GameObject player = Instantiate(prefab, transform);
            player.name = "Player: " + id;

            PlayerData pData = new PlayerData();
            playerList.Add(id, pData);

            Debug.Log(player.name + " has been added to the game");

            JoinGame(id);
        }
    }
    public void FixedUpdate()
    {
        if (playerDataToChange.Count > 0)
        {
            PlayerData data = playerDataToChange.Dequeue();
            //Change Data Operation
            //Sync Operation with Clients
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

    //not used by server
    private void SpawnPlayer(int connectionID)
    {
        GameObject player = Instantiate(prefab, transform);
        player.name = "Player: " + connectionID;

        PlayerData pData = new PlayerData();
        playerList.Add(connectionID, pData);

        Debug.Log(player.name + " has been added to the game");
    }
    #endregion
}
