using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickup : MonoBehaviour
{    
    int LootRarity = 0;
    public MaterialPickup(int MatType)
    {
        //apparently there is no option to add a component with a constructor to a game object 
        //this makes me sad
        LootRarity = MatType;
    }
    public void SetType(int MatType)
    {
        LootRarity = MatType;
    }

    public void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            LootHolder playerInventory = collision.gameObject.GetComponent<LootHolder>();
            if (playerInventory == null)
            {
                Debug.Log("Player Inventory is null and cannot be detected!");
                return;
            }
            if (playerInventory.Inventory[LootRarity].quantity >= 99)
            {
                Debug.LogWarning("The player has too many materials and cannot pick this up!");
                //Play sound
                return;
            }
            //Play sound
            playerInventory.GainLoot(LootRarity);
            Destroy(gameObject);
        }
    }
}
