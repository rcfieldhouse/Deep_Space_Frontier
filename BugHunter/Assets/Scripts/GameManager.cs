using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBarFill;

    public void BulletTime()
    {
         
    }

    public void SceneChange(string sceneName)
    {
        //Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
       
    }

    public void SceneChange(int sceneID)
    {
        //Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(sceneID, LoadSceneMode.Additive);
    }
    public void SceneChangeAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
    }

    public void ScenePreLoad()
    {

    }
    public void ScenePostLoad()
    {

    }
}
