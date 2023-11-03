using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    //TODO: will need to refactor this at some point.
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;

    [Header("Time Slow")]
    public float timeSlowStrength = 0.05f;
    public float timeSlowDuration = 1f;

    private static FMOD.Studio.EventInstance Ambience;
    private static FMOD.Studio.EventInstance Music;

    [SerializeField]
    private string m_SceneName;

    public bool isHost;
    public string IP = "127.0.0.1";

    bool skippedCutscene = false;

#if UNITY_EDITOR
    public UnityEditor.SceneAsset SceneAsset;
    private void OnValidate()
    {
        if (SceneAsset != null)
        {
            m_SceneName = SceneAsset.name;
        }
    }
#endif

    public void SetIP(string ip)
    {
        IP = ip;
    }
    public void SetHost(bool host)
    {
        isHost = host;
    }
    private void Awake()
    {
        if(instance==null)
        instance = this;
        ChangeSceneMusic();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;        
    }
    
    private void ChangedActiveScene(Scene arg0, Scene arg1)
    {
        NetworkManager.Singleton.Shutdown();
        Debug.Log("NETWARKING");
        NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address = IP;
        StartCoroutine(DelayNetwork());

       
        Debug.Log("NETWARKING!!!!");
        ChangeSceneMusic();
    }

    public void SkipScene()
    {
        StopAllCoroutines();
        StartCoroutine(ImmediateNetwork());
    }

    private IEnumerator DelayNetwork()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

                yield return new WaitForSeconds(40.0f);

            if (isHost)
            {
                NetworkManager.Singleton.StartHost();
            }
            else
            {

                NetworkManager.Singleton.StartClient();
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            if (isHost)
            {
                NetworkManager.Singleton.StartHost();
            }
            else
            {

                NetworkManager.Singleton.StartClient();
            }
        }

       
    }
    private IEnumerator ImmediateNetwork()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            yield return new WaitForSeconds(0.5f);

            if (isHost)
            {
                NetworkManager.Singleton.StartHost();
            }
            else
            {

                NetworkManager.Singleton.StartClient();
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            if (isHost)
            {
                NetworkManager.Singleton.StartHost();
            }
            else
            {

                NetworkManager.Singleton.StartClient();
            }
        }


    }
    private static void ChangeSceneMusic()
    {
        Debug.Log("NETWARKING@@@@@");
        FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Ambience_Outdoor");
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/DeepSpace");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);

            Music.start();
            Music.release();

            Ambience.start();
            Ambience.release();
            NetworkManager.Singleton.gameObject.transform.position = new Vector3(910, 7, 943);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5) //|| SceneManager.GetActiveScene().buildIndex == 8)
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Odyssey");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);

            Music.start();
            Music.release();
            NetworkManager.Singleton.gameObject.transform.position = new Vector3(1014, -29, 916);

        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Digital Jungle");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);
            Music.start();

            Ambience.start();
            Ambience.release();
            NetworkManager.Singleton.gameObject.transform.position = new Vector3(100, 920, -993);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TitleScreen");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);

            Music.start();
            Music.release();
        }
        Debug.Log("NETWARKING#####");
    }


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
    public IEnumerator SceneChangeAsyncWDelay(string sceneName, float Time)
    {
        Debug.Log("Called");
        yield return new WaitForSeconds(Time);

        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();
        FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
        Debug.Log("new scene loaded");
        
        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
    }
    public void SceneChange(string sceneName, float Time)
    {
        StartCoroutine(SceneChangeAsyncWDelay(sceneName,Time));
    }
    public void SceneChange(string sceneName)
    {
        Debug.Log("Called");
        StartCoroutine(SceneChangeAsync(sceneName));
    }

    public void SceneChange(int sceneID)
    {
        StartCoroutine(SceneChangeAsync(sceneID));
    }
    public IEnumerator SceneChangeAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();
        FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }


        loadingScreen.SetActive(false);
        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
    }
    public IEnumerator SceneChangeAsync(string sceneName)
    {
        Debug.Log("new scene");

        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();
        FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
        Debug.Log("new scene loaded");
        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
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
