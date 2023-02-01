using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAmmo : MonoBehaviour
{
    [Range(0,50)] public int Damage = 10;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision with player detected");
            other.gameObject.GetComponent<HealthSystem>().ModifyHealth(-Damage);
        }


        if (other.tag != "Enemy")
        {
            // Debug.Log("orb has entered " + other.name);
            Destroy(gameObject);
        }     
    }
}
