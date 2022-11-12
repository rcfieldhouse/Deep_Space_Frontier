using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("yahoo");
            AmmoManager.instance.setAmmoCount();
            Destroy(gameObject);
        }
    }
}
