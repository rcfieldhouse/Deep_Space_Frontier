using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickup : MonoBehaviour
{
    
    int TypeOfLoot = 0;
    public MaterialPickup(int MatType)
    {
        //apparently there is no option to add a component with a constructor to a game object 
        //this makes me sad
        TypeOfLoot = MatType;
    }
    public void SetType(int MatType)
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
