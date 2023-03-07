using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<HealthSystem>().GetMaxHealth() == collision.gameObject.GetComponent<HealthSystem>().GetHealth())
            {
                Destroy(gameObject);
                return;
            }
            collision.gameObject.GetComponent<PlayerDamageIndicator>().HealthRegen.GetComponent<ParticleSystem>().Play();
            int percent =  collision.gameObject.GetComponent<HealthSystem>().GetMaxHealth()/4;
            collision.gameObject.GetComponent<HealthSystem>().ModifyHealth(null, percent);
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<HealthSystem>().GetMaxHealth() == collision.gameObject.GetComponent<HealthSystem>().GetHealth())
            {
                Destroy(gameObject);
                return;
            }
            collision.gameObject.GetComponent<PlayerDamageIndicator>().HealthRegen.GetComponent<ParticleSystem>().Play();
            int percent = collision.gameObject.GetComponent<HealthSystem>().GetMaxHealth() / 4;
            collision.gameObject.GetComponent<HealthSystem>().ModifyHealth(null, percent);
            Destroy(gameObject);
        }
    }
}
