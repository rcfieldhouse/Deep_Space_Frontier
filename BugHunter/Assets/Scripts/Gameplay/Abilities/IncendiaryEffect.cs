using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncendiaryEffect : MonoBehaviour
{
    //Timer is total time, ticker is how often the subject takes damage, Tick is the iterator
    private float BurnTimer = 5.0f, BurnTicker=0.25f, BurnTick=0.0f;
    private int Damage = -8;
    public GameObject VFX;
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
        GetComponentInParent<AI>().transform.GetChild(2).GetComponent<FireID>().gameObject.SetActive(true);
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
                if (GetComponent<DamageIndicator>())
                {
                    Transform transform = GetComponent<DamageIndicator>().DamageReceivedFrom;
                    gameObject.AddComponent<DamageIndicator>().SetIndicator(transform, Damage,false);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Sniper_Fire");
                }
                // damage per burn tick
                gameObject.GetComponent<HealthSystem>().ModifyHealth(transform,Damage);
                BurnTick += BurnTicker;
            }
            yield return null;
        }
        GetComponentInChildren<FireID>().gameObject.SetActive(false);
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        Destroy(this);
        yield return null;
    }
}
