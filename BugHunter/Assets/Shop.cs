using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject ShopInterface;
    private GameObject Player;
    private GUIHolder gui;
    private PlayerInput PlayerInput;
    public ShopType TypeOfShop;
    // Start is called before the first frame update
    public enum ShopType
    {
        LevelSelect, EquipmentLocker, CraftingMenu
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.transform.gameObject;
            gui = Player.transform.parent.GetComponentInChildren<GUIHolder>();
            gui.PickupPrompt.SetActive(true);
            //ShopInterface = gui.CraftingMenu;
            if(TypeOfShop==ShopType.CraftingMenu)
                ShopInterface = gui.CraftingMenu;
            if(TypeOfShop==ShopType.EquipmentLocker)
                ShopInterface = gui.EquipmentLocker;
            if (TypeOfShop == ShopType.LevelSelect)
                ShopInterface = gui.LVLSelect;
            PlayerInput = Player.GetComponent<PlayerInput>();
            PlayerInput.Interact += ToggleShop;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gui = Player.transform.parent.GetComponentInChildren<GUIHolder>();
            gui.PickupPrompt.SetActive(false);
            // added this to disable the shop canvas
            ShopInterface.SetActive(false);
            ShopInterface = null;
            Cursor.lockState = CursorLockMode.Locked;
            Player = null;
            // commented this out since I couldn't re-open canvases in game because of it
            //ShopInterface = null;
            PlayerInput.Interact -= ToggleShop;
            PlayerInput = null;
            gui = null;
        }
    }

    public void ToggleShop()
    {
        if (Player == null)
            return;

        Debug.Log("Shop is Opened :(");
        bool toggle = !ShopInterface.activeInHierarchy;
        // i only need the canvas to activate not everything attached to it for tab switching
        //ShopInterface.SetActiveRecursively(!ShopInterface.activeInHierarchy);
        ShopInterface.SetActive(toggle);

       

        gui.PickupPrompt.SetActive(!toggle);

         Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetPaused(toggle);
         Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(toggle);
         Player.transform.parent.GetComponentInChildren<Look>().SetIsPaused(toggle);
        //this is only so that esc is able to close the vendor for the sake of user integrity
        
        
        

        if (toggle == true) Cursor.lockState = CursorLockMode.None;
        else if (toggle == false) Cursor.lockState = CursorLockMode.Locked;
    }

    public void ValidatePurchase()
    {
        throw new System.NotImplementedException();
    }
    public void AwardItem()
    {

    }


}
