using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HealthSystem>().GetHealth() <= 0)
        {
            GameManager.instance.SceneChange("LooseScreen");
            //SavePlugin2.instance.LoadItems();
        }
    }
}
