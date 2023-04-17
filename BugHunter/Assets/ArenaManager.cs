using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ArenaManager : MonoBehaviour
{
    public int WaveNum,NumEnemies;
    public static ArenaManager instance;
    public static Action<int> NewWave;
    private WaitForSeconds LoopTime = new WaitForSeconds(5.0f);

    private void Awake()
    {
        if (!instance)
            instance = this;
        StartCoroutine(WackyUpdate());

    }
    private void Start()
    {
        
       
    }
    // Update is called once per frame
    public IEnumerator WackyUpdate()
    {
        
        while (true)
        {
            
            yield return LoopTime;
            Debug.Log("DOOTDOOT");
            if (NumEnemies == 0)
            {
                Debug.Log("DikaDika");
                WaveNum++;
                NewWave.Invoke(WaveNum);
            }

        }
    }
}
