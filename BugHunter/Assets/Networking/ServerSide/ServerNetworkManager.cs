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
            Debug.Log("IDToSet Triggered");
            int id = _IDToSet.Dequeue();

            PlayerData pData = new PlayerData();
            GameObject player = Instantiate(prefab, transform);
            player.name = "Player: " + id;

            playerList.Add(id, pData);

            Debug.Log(player.name + " has been added to the game");

            JoinGame(id);
        }
    }

    public void FixedUpdate()
    {
        //Post Update Physics
        if (playerToUpdate.Count > 0)
        {
            //Reconciliation here
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

#endregion
    public void MovementPrediction()
        {
            //Synchronise client clocks with the Server Clocks
    
            //OnTick for pre-Physics calcs
            //    movement command from client (this is to improve preserved latency i.e. movement prediction)
            //    client does a tentative movement
    
            //PostOnTick post physics
            //    player moves on serverside
            //    Server corrects position if necessary
            //    client updates to corrected position
            //    Timestamps (found in data packet header) determine where client should be ona  particular frame
            //    Velocity is used to bridge the gap between server packet send rate
    
        }
}
