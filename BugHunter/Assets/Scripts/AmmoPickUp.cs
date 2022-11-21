using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WeaponInfo info = GameObject.Find("WeaponHolder").GetComponentInChildren<WeaponInfo>();
            if (info.GetMaxBullets() / 33 > 1) info.SetReserveAmmo(info.GetMaxBullets() / 33);
            else info.SetReserveAmmo(info.GetReserveAmmo() + 1);

            Destroy(gameObject);
        }
    }
}
