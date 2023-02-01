using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int percent =  collision.gameObject.GetComponent<HealthSystem>().GetMaxHealth()/4;
            collision.gameObject.GetComponent<HealthSystem>().ModifyHealth(null, percent);
            Destroy(gameObject);
        }
    }
}
