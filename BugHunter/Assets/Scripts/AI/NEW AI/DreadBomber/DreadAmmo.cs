using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreadAmmo : MonoBehaviour
{

    private int Damage = -20;
  
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
        }
        if (other.tag == "Player" || other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    public void SetDamage(int dmg)
    {
        Damage = dmg;
    }
}
