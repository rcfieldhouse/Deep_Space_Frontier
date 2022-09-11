using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

//this script is used on a empty that manages the items in the player's inventory by using a list
// additionally assists in adding the items from the list into the player's UI inventory
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    // List of our inventory items
    public List<Item> Items = new List<Item>();

    // where the items are filled in our UI inventory
    public Transform Content;
    // the 2D prefab version of the item
    public GameObject InventoryItem2D;

    private void Awake()
    {
        Instance = this;
    }
    // add an item to the iventory
    public void Add(Item item)
    {
        Items.Add(item);
    }
    // remove an item from the inventory
    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    // cleans then updates UI inventory representation depending on how many items are in the
    // list of items
    public void ListItems()
    {
        // clean old items from previous inventory launch
        foreach(Transform item in Content)
        {
            Destroy(item.gameObject);
        }
        // for each of us items in the inventory list grab their name and icon from the scritable object
        // then change the UI inventory representation of them accordingly
        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem2D, Content);

            var itemName = obj.transform.Find("ItemName_TMP").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

        }
    }
}
