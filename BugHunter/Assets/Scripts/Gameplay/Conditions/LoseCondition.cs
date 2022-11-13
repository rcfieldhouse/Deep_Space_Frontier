using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
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
            //Lose Condition
            //revamp later
            Debug.Log("Skill Issue tbh");
            SavePlugin2.instance.LoadItems();
        }
    }
}
