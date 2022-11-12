using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickup : MonoBehaviour
{
    int TypeOfLoot = 0;
    public MaterialPickup(int MatType)
    {
        TypeOfLoot = MatType;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LootHolder.instance.GainLoot(TypeOfLoot);
            Destroy(gameObject);
        }
    }
}
