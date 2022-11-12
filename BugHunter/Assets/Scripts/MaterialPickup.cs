using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickup : MonoBehaviour
{
    public MaterialPickup(int MatType)
    {

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LootHolder.instance.GainLoot();
            Destroy(gameObject);
        }
    }
}
