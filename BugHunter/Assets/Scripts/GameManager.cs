using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //TODO: will need to refactor this at some point.
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;

    [Header("Time Slow")]
    public float timeSlowStrength = 0.05f;
    public float timeSlowDuration = 1f;

    private void Awake()
    {
        if(instance==null)
        instance = this;
        DontDestroyOnLoad(this.gameObject);
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

        yield return new WaitForSeconds(Time);
        GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();
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
        GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
    }
    public void SceneChange(string sceneName, float Time)
    {
        StartCoroutine(SceneChangeAsyncWDelay(sceneName,Time));
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
        Debug.Log("new scene");
        GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();
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
        GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
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
