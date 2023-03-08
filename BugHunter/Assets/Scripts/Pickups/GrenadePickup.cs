using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [Range(0, 5)] public int numGrenades=1;
    bool Triggered=false;
    // Start is called before the first frame update
    void KillThis()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Player"&&Triggered==false)
        {
            Invoke(nameof(KillThis), 0.1f);
            Triggered = true;    
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);
            collision.transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
          
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Triggered == false)
        {
            Invoke(nameof(KillThis), 0.1f);
            Triggered = true;
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);
            collision.transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Triggered == false)
        {
            Invoke(nameof(KillThis), 0.1f);
            Triggered = true; 
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);
            collision.transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
       
        }
    }
}
