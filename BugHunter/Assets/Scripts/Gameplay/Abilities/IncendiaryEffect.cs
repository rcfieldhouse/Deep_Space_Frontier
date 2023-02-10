using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncendiaryEffect : MonoBehaviour
{
    //Timer is total time, ticker is how often the subject takes damage, Tick is the iterator
    private float BurnTimer = 5.0f, BurnTicker=0.25f, BurnTick=0.0f;
    private int Damage = -8;
    // Start is called before the first frame update

    public void SetValues(Vector3 vec)
    {
        Damage = (int)vec.x;
        BurnTimer = vec.y;
        BurnTicker = vec.z;
    }
    private void Awake()
    {
        Invoke(nameof(Effect), 0.05f);
    }
    // Update is called once per frame
    private void Effect()
    {
        StartCoroutine(BurnDeBoi());
    }
    private IEnumerator BurnDeBoi()
    {
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
         float Timer=0; 
        while (Timer < BurnTimer)
        {
            Timer += Time.deltaTime;
            if (Timer > BurnTick)
            {
                // damage per burn tick
                gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
                BurnTick += BurnTicker;
            }
            yield return null;
        }
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        Destroy(this);
        yield return null;
    }
}
