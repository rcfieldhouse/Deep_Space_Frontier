using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChanger : MonoBehaviour
{
    public void TitleScreenLoad()
    {
        GameManager.instance.SceneChange("TitleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}