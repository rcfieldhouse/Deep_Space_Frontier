using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticTracker : MonoBehaviour
{
    public static StatisticTracker instance;
    //float accuracy = 100;
    int total_shots = 0;
    //int total_hits = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
           
    }
    public void ShotsFired()
    {
        total_shots++;
        Debug.Log(total_shots);
    }
   
}
