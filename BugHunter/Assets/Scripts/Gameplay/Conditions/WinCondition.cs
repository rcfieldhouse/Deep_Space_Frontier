using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private bool Condition = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HealthSystem>().GetHealth() <= 0 &&Condition==false)
        {
            WinConditionMet();
            Condition = true;
        }
    }
    public void WinConditionMet()
    {
        StopAllCoroutines();

        SceneManager.LoadSceneAsync("Hub");
     //   GameManager.instance.SceneChange("Hub");    
    }
  
}
