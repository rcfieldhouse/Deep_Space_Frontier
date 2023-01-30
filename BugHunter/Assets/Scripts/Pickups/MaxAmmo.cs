using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmo : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject obj = collision.transform.parent.GetChild(1).GetComponentInChildren<WeaponSwap>().gameObject;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                obj.transform.GetChild(i).gameObject.GetComponent<WeaponInfo>().SetReserveAmmo(obj.transform.GetChild(i).gameObject.GetComponent<WeaponInfo>().GetMaxBullets());
            }
         
            Destroy(gameObject);
        }
    }
}
