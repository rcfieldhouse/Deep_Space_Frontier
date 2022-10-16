using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epickup : MonoBehaviour
{
    // this script is attached to collectable items and adds an item to the list when colliding with the player
    [SerializeField] private LayerMask whatIsPlayer;
    public Item Item;
    [SerializeField] private float PickupRange;

    public void Start()
    {
        PlayerInput.PickupItem += Pickup;
    }
    // adds the item to the inventory list then destroys it's self
    void Pickup()
    {
      if(Physics.CheckSphere(transform.position, PickupRange, whatIsPlayer)==true)
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }
      
    }
    // when the player collides with this object call the pickup function
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PickupRange);

    }
}
