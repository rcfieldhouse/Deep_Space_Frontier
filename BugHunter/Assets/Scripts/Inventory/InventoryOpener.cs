using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used to open the inventory using a button press
// and call the inventory manager script when inventory is opened
public class InventoryOpener : MonoBehaviour
{
    public GameObject inventoryUI;
    public InventoryManager iManager;

    public void Update()
    {
        openInventory();
    }

    public void openInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            iManager.ListItems();
        }
    }
}
