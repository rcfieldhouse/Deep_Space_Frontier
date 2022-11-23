using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBarrier : MonoBehaviour
{
    private GameObject nade=null;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "grenade")
        {
            nade = collision.gameObject;
            
        }
    }
    void Update()
    {
        if(nade)
        if (nade.GetComponent<GrenadeThrow>().GetIsExploding() == true)
        {
            Destroy(gameObject);
        }
       
    }
}
