using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    private Queue<int> _IDToSet = new Queue<int>();

    public GameObject prefab;

    //TODO: will need to refactor this at some point.
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;

    [Header("Time Slow")]
    public float timeSlowStrength = 0.05f;
    public float timeSlowDuration = 1f;

    public static Queue<Server.MovementCommand> _movementQueue = new Queue<Server.MovementCommand>();

    public static Queue<Server.LookCommand> _lookQueue = new Queue<Server.LookCommand>();

    private void Awake()
    { 
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

#region Server
    private void Update()
    {

        //Must Manipulate Objects within Main thread!
        while (_IDToSet.Count > 0)
        {
            int id = _IDToSet.Dequeue();

            GameObject player = Instantiate(prefab, transform);
            player.name = "Player: " + id;
            playerList.Add(id, player);

            Debug.Log(player.name + " has been added to the game");

            JoinGame(id);
        }
    }
    public void FixedUpdate()
    {
        if (_movementQueue.Count > 0)
        {
            Server.MovementCommand cmd = _movementQueue.Dequeue();
            playerList[cmd.connectionID].GetComponentInChildren<PlayerInput>().MoveInput(cmd.vector);
            Server.NetworkSend.SendPlayerMove(cmd.connectionID, playerList[cmd.connectionID].transform.position);
        }

        if (_lookQueue.Count > 0)
        {
            Server.LookCommand cmd = _lookQueue.Dequeue();
            //this vector is not calculated right
            playerList[cmd.connectionID].GetComponentInChildren<PlayerInput>().LookInput(cmd.vector);
            Server.NetworkSend.SendPlayerRotation(cmd.connectionID, playerList[cmd.connectionID].transform.rotation);
        }
    }

    public void JoinGame(int connectionID)
    {
        Server.NetworkSend.InstantiateNetworkPlayer(connectionID);
    }

    public void CreatePlayer(int connectionID)
    {
        _IDToSet.Enqueue(connectionID);
    }

    private void SpawnPlayer(int connectionID)
    {
        GameObject player = Instantiate(prefab, transform);
        player.name = "Player: " + connectionID;
        playerList.Add(connectionID, player);
        Debug.Log(player.name + " has been added to the game");
    }
#endregion

#region GameWorld
    public void BulletTime()
    {
        Time.timeScale = timeSlowStrength;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void StopTime()
    {
        Time.timeScale = 0.0f;        
        Time.fixedDeltaTime = 0.0f;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }


    public void SceneChange(string sceneName)
    {       
        StartCoroutine(SceneChangeAsync(sceneName));
    }

    public void SceneChange(int sceneID)
    {
        StartCoroutine(SceneChangeAsync(sceneID));
    }
    public IEnumerator SceneChangeAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
    public IEnumerator SceneChangeAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    internal float WrapEulerAngles(float rotation)
    {
        rotation %= 360;
        if (rotation >= 180)
            return -360;
        return rotation;
    }
    public float UnwrapEulerAngles(float rotation)
    {
        if (rotation >= 0)
            return rotation;

        rotation = -rotation % 360;
        return 360 - rotation;
    }
    public void ScenePreLoad()
    {

    }
    public void ScenePostLoad()
    {

    }
#endregion

}
