using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WinConditionMet()
    {
        SceneManager.LoadScene("WinScreen",LoadSceneMode.Single);
       // SceneManager.UnloadScene("SampleScene");
     
    }
  
    public void OnDisable()
    {
        WinConditionMet();
    }
}
