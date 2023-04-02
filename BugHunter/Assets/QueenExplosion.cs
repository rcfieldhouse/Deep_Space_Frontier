using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenExplosion : MonoBehaviour
{
    bool DoneDmg = false;
    [Range(0, 5)] public float VenomDamageTime = 5.0f, VenomDamageInterval = 0.25f;
    [Range(0, -10)] public int VenomDamage = -5;
    private void Awake()
    {
        Invoke(nameof(killthis), 1.0f);
    }
   void killthis()
    {
        Destroy(transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (DoneDmg)
                return;
            other.gameObject.GetComponent<HealthSystem>().ModifyHealth(gameObject, -3);

            if (other.gameObject.GetComponent<Venom>() == null)
                other.gameObject.AddComponent<Venom>().InitAttack(VenomDamageTime, VenomDamageInterval, VenomDamage);
            DoneDmg = true;
        }
    }
}
