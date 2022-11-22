using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           HealthSystem temp= collision.gameObject.GetComponent<HealthSystem>();
            collision.gameObject.GetComponent<HealthSystem>().ModifyHealth(temp.GetMaxHealth() - temp.GetHealth());
            Destroy(gameObject);
        }
    }
}
