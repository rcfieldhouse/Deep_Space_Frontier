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
        GameManager.instance.SceneChange("TitleScreen");
    }
    public void HubLoad()
    {
        GameManager.instance.SceneChange("Hub");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}