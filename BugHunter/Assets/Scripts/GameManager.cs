using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerData> playerList = new Dictionary<int, PlayerData>();
    public Queue<int> _IDToSet = new Queue<int>();

    public Queue<PlayerData> playerDataToChange = new Queue<PlayerData>();

    public GameObject prefab;

    //TODO: will need to refactor this at some point.
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;

    [Header("Time Slow")]
    public float timeSlowStrength = 0.05f;
    public float timeSlowDuration = 1f;

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
