using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("sf");
            WeaponInfo info = collision.transform.parent.GetComponentInChildren<WeaponInfo>();
            if (info.GetMaxBullets() *0.25 > 1) info.SetReserveAmmo(info.GetReserveAmmo ()+(int)(info.GetMaxBullets() * 0.25));
            else info.SetReserveAmmo(info.GetReserveAmmo() + 1);

            Destroy(gameObject);
        }
    }
}
