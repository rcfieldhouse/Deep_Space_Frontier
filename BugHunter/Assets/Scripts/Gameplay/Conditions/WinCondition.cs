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
        if (GetComponent<HealthSystem>().GetHealth() <= 0)
        {
            WinConditionMet();
        }
    }
    public void WinConditionMet()
    {
        GameManager.instance.SceneChange("WinScreen");    
    }
  
    public void OnDisable()
    {
        WinConditionMet();
    }
}
