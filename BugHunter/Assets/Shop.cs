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
    void Start()
    {
        PlayerInput.Interact += ToggleShop;
        

    }
    private void OnDisable()
    {
        PlayerInput.Interact -= ToggleShop;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.transform.gameObject;
            gui = Player.transform.parent.GetComponentInChildren<GUIHolder>();
            gui.PickupPrompt.SetActive(true);
            PlayerInput = Player.GetComponent<PlayerInput>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gui = Player.transform.parent.GetComponentInChildren<GUIHolder>();
            gui.PickupPrompt.SetActive(false);
            Player = null;
            PlayerInput = null;
        }
    }

    public void ToggleShop()
    {
        if (Player == null)
            return;

        ShopInterface.SetActiveRecursively(!ShopInterface.activeInHierarchy);

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
