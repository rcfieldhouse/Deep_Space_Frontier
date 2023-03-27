using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickup : MonoBehaviour
{    
    int LootIndex = 0;
    public MaterialPickup(int MatType)
    {
        //apparently there is no option to add a component with a constructor to a game object 
        //this makes me sad
        LootIndex = MatType;
    }
    public void SetType(int MatType)
    {
        LootIndex = MatType;
        ChangeMaterial();
    }

    public void ChangeMaterial()
    {
        switch (LootIndex)
        {
            case 1:
            case 4: 
            case 7:
            case 10:
                this.GetComponent<MeshRenderer>().material.color =  Color.white;
                break;
            case 2:
            case 5:
            case 8:
            case 11:
                this.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 3:
            case 6:
            case 9:
            case 12:
                this.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
        }
    }



    public void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            LootHolder playerInventory = collision.gameObject.GetComponent<LootHolder>();
            if (playerInventory == null)
            {
                Debug.LogWarning("Player Inventory is null and cannot be detected!");
                return;
            }
            if (playerInventory.Inventory[LootIndex].Quantity >= 99)
            {
                Debug.LogWarning("The player has too many materials and cannot pick this up!");
                return;
            }
            FMODUnity.RuntimeManager.PlayOneShot("event:/Pickups/Pickup_Resource");
            playerInventory.GainLoot(LootIndex);
            Destroy(gameObject);
        }
    }
}
