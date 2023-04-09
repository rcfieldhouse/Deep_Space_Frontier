using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// this script is attached to collectable items and adds an item to the list when colliding with the player
public class PickupItems : MonoBehaviour
{
    public Item Item;
    public GameObject gem;
    public GameObject pos;
    // adds the item to the inventory list then destroys it's self
    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
    // when the player collides with this object call the pickup function
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Pickup();
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.J))
        {
            for(int i = 0; i < 500; i++)
            {
                Instantiate(gem, pos.transform);
                
            }
        }
    }
}
