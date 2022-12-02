using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChanger : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TitleScreenLoad()
    {
        Debug.Log("doot");
        GameManager.instance.SceneChange("TitleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}