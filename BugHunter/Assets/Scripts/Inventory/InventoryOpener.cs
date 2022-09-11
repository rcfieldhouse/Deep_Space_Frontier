using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is used to open the inventory using a button press
// and call the inventory manager script when inventory is opened
public class InventoryOpener : MonoBehaviour
{
    public GameObject inventoryUI;
    public Toggle RemovalToggle;
    public InventoryManager iManager;

    public void Update()
    {
        openInventory();
    }
    // when the player opens inventory set the states of the inventory to their default state
    public void openInventory()
    {
        if(inventoryUI.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryUI.SetActive(true);
                RemovalToggle.isOn = false;
                Cursor.lockState = CursorLockMode.None;
                iManager.ListItems();
            }
        }
    }
}
