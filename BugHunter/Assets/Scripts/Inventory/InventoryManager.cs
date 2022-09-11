using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

//this script is used on a empty that manages the items in the player's inventory by using a list
// additionally assists in adding the items from the list into the player's UI inventory
public class InventoryManager : MonoBehaviour
{
    // create an public static instance so other scripts can use this script's functions
    public static InventoryManager Instance;
    // List of our inventory items
    public List<Item> Items = new List<Item>();
    public InventoryItemRemover[] InventoryItemArray;

    // where the items are filled in our UI inventory
    public Transform Content;
    // the 2D prefab version of the item
    public GameObject inventoryItem2D;

    public Toggle enableItemRemoval;

    private void Awake()
    {
        Instance = this;
    }
    // add an item to the iventory list
    public void Add(Item item)
    {
        Items.Add(item);
    }
    // remove an item from the inventory list
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
            GameObject obj = Instantiate(inventoryItem2D, Content);

            var itemName = obj.transform.Find("ItemName_TMP").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        // when our list of items updates so does our array
        SetInventoryArray();
    }
    //This is called when the Delete Items toggle in the inventory menu is clicked
    // when its clicked on it will unhide the remove button for each inventory item
    // when it's clicked off it will hide the remove button for each inventory item
    public void DeleteInventoryItems()
    {
        //Unhide removal buttons
        if(enableItemRemoval.isOn)
        {
            foreach(Transform item in Content)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        //Hide removal buttons
        else
        {
            foreach (Transform item in Content)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }
    
    public void SetInventoryArray()
    {
        //set our array to be equal to all children in content who have the component item remover (the 2D prefab items)
        InventoryItemArray = Content.GetComponentsInChildren<InventoryItemRemover>();
        //Go through each item in the inventory List and add it to the array
        for(int i = 0; i < Items.Count; i++)
        {
            InventoryItemArray[i].AddItem(Items[i]);
        }
    }

}
