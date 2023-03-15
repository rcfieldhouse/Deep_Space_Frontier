using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemRemover : MonoBehaviour
{
    //public Button RemovalButton;
    Item Item;

    public void RemoveItem()
    {
        // remove the item from the list
        InventoryManager.Instance.Remove(Item);
        // destroy this item
        Destroy(gameObject);
    }
    // Is called by the item array in InventoryManager.cs
    public void AddItem(Item newItem)
    {
        Item = newItem;
    }
}
