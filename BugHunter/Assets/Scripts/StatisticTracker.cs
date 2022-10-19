using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticTracker : MonoBehaviour
{
    public static StatisticTracker instance;
    float accuracy = 100;
    float total_shots = 0f;
    float total_hits = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance==null)
        {
            instance = this;
        }
           
    }
    public void ShotsFired()
    {
        total_shots++;
        //Debug.Log(total_shots);
    }
    public void ShotsHit()
    {
        total_hits++;
        //Debug.Log(total_hits);
    }
    public void Accuracy()
    {
        accuracy = (total_hits / total_shots * 100);
        Debug.Log("Sniper accuracy: " + accuracy + "%");
        //Debug.Log(total_hits);
    }
   
}
