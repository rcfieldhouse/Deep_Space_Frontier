using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopInterface;
    
    public GameObject Player;
    public GUIHolder gui;
    private PlayerInput PlayerInput;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.transform.gameObject;
            gui = Player.transform.parent.GetComponentInChildren<GUIHolder>();
            gui.PickupPrompt.SetActive(true);
            //ShopInterface = gui.CraftingMenu;
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
            Cursor.lockState = CursorLockMode.Locked;
            Player = null;
            // commented this out since I couldn't re-open canvases in game because of it
            //ShopInterface = null;
            PlayerInput.Interact -= ToggleShop;
            PlayerInput = null;
        }
    }

    public void ToggleShop()
    {
        if (Player == null)
            return;
        Debug.Log("Shop is Opened :(");

        // i only need the canvas to activate not everything attached to it for tab switching
        //ShopInterface.SetActiveRecursively(!ShopInterface.activeInHierarchy);
        ShopInterface.SetActive(true);

        bool toggle = ShopInterface.activeInHierarchy;

        gui.PickupPrompt.SetActive(false);
        //this is only so that esc is able to close the vendor for the sake of user integrity
        Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetCanShoot(!toggle);
        Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(toggle);
        Player.transform.parent.GetComponentInChildren<Look>().SetIsPaused(!toggle);

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
