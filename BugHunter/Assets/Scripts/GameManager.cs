using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
   
    //TODO: will need to refactor this at some point.
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;

    [Header("Time Slow")]
    public float timeSlowStrength = 0.05f;
    public float timeSlowDuration = 1f;

    public void BulletTime()
    {
        Time.timeScale = timeSlowStrength;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void Update()
    {
        Time.timeScale += (1f / timeSlowStrength) * Time.deltaTime;
        Time.timeScale = Mathf.Clamp01(Time.timeScale);
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
      
    }


    public void ScenePreLoad()
    {

    }
    public void ScenePostLoad()
    {

    }
}
