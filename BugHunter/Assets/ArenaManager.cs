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
    // Update is called once per frame
    public IEnumerator WackyUpdate()
    {
        while (true)
        {
            yield return LoopTime;
            if (NumEnemies == 0)
            {
                WaveNum++;
                NewWave.Invoke(WaveNum);
            }

        }
    }
}
