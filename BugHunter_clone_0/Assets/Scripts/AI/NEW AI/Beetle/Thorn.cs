using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    // Start is called before the first frame update

    private int Damage = -20;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<HealthSystem>().ModifyHealth(gameObject,Damage);
        }
        if (other.tag == "Player"||other.tag=="Ground")
        {        
            Destroy(gameObject);
        }
        
    }
    public void SetDamage(int dmg)
    {
        Damage = dmg;
    }
}