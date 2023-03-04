using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [Range(0, 5)] public int numGrenades=1;
    bool Triggered=false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"&&Triggered==false)
        {
            Triggered = true;    
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);
            collision.transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Triggered == false)
        {
            Triggered = true;
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);
            collision.transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Triggered == false)
        {
            Triggered = true; 
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);
            collision.transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
            Destroy(gameObject);
        }
    }
}
