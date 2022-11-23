using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncendiaryEffect : MonoBehaviour
{
    //Timer is total time, ticker is how often the subject takes damage, Tick is the iterator
    private float BurnTimer = 5.0f,BurnTicker=0.25f,BurnTick=0.0f;
    // Start is called before the first frame update

    private void Awake()
    {
        StartCoroutine(BurnDeBoi());
    }
    // Update is called once per frame

    private IEnumerator BurnDeBoi()
    {
         float Timer=0; 
        while (Timer < BurnTimer)
        {
            Timer += Time.deltaTime;
            if (Timer > BurnTick)
            {
                gameObject.GetComponent<HealthSystem>().ModifyHealth(-2);
                BurnTick += BurnTicker;
            }
            yield return null;
        }
        Destroy(this);
        yield return null;
    }
}
