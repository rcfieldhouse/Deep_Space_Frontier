using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class NPC : MonoBehaviour
{
    //using abstract class so that all vendors can derrive from this
    //also doing all of the like behaviours in the abstract so that they do not to be written twice
    public GameObject Player;
    public bool UI_Active;
    public static Action<String,bool> SelectUI;

    public GUIHolder gui;

    private void Awake()
    {
        gui = GetPlayer().transform.parent.GetComponentInChildren<GUIHolder>();
        UI_Active = false;

        PlayerInput player = GetPlayer().GetComponent<PlayerInput>();
        //temp solution until we have more players
        //Player = FindObjectOfType<PlayerInput>().gameObject;

    }

    public GameObject GetPlayer()
    {
        return Player;
    }
  

    public abstract void VendorUI();
    public abstract void VendorAction();
    public abstract void ToggleVendor();
    public abstract void CloseVendor();

    public void ToggleVendorUI(bool var)
    {
        SelectUI.Invoke(Name,var);
    }
    public void ToggleAimOnPlayer(bool var)
    {
        if (Player != null)
        { 
            Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetCanShoot(!var);
            Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(var);
            Player.transform.parent.GetComponentInChildren<Look>().SetIsPaused(var);
            if (var == true) Cursor.lockState = CursorLockMode.None;
            else if (var == false) Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void UnlockPlayerInputs()
    {
        if (Player != null&&UI_Active==true)
        {
            //this is only so that esc is able to close the vendor for the sake of user integrity
            Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetCanShoot(true);
            Player.transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(false);
            Player.transform.parent.GetComponentInChildren<Look>().SetIsPaused(false);
            Invoke(nameof(wait), 0.1f);             
        }
    }
    private void wait()
    {   
        GetPlayer().transform.parent.GetComponentInChildren<UIManager>().ResumeGame();
    }
    public abstract string Name { get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(Name);
            Player = other.transform.gameObject;
            Player.transform.parent.GetComponentInChildren<GUIHolder>().PickupPrompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.transform.parent.GetComponentInChildren<GUIHolder>().PickupPrompt.SetActive(false);
            Player = null;
   
        }
    }
}




public class Merchant : NPC
{
    private void Start()
    {
        PlayerInput.Interact += ToggleVendor;
        PlayerInput.PausePlugin += UnlockPlayerInputs;
        PlayerInput.PausePlugin += CloseVendor;
    }
    private void OnDestroy()
    {
        PlayerInput.Interact -= ToggleVendor;
        PlayerInput.PausePlugin -= UnlockPlayerInputs;
        PlayerInput.PausePlugin -= CloseVendor;
    }
    //merchant can be used to sell parts for currency
    public override void VendorUI()
    {
        GameObject Shop = GameObject.Find("ShopUI");
        Shop.SetActive(!Shop.activeSelf);
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
    }

    public override void ToggleVendor()
    {
        if (GetPlayer() != null)
        {
            Debug.Log("I am Toggling the VendorUI");
            gui.GUI.SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
        }
    }
    public override void CloseVendor()
    {
        if (GetPlayer() != null)
        {
            gui.GUI.SetActive(false);
            UI_Active = false;
            ToggleVendorUI(false);
        }
    }
    public override string Name => "Merchant";
}
public class Healer : NPC
{
    //healer can be used to heal urself obvi
    //possible max health increase idk

    public override void VendorUI()
    {
 
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
    }
    public override void ToggleVendor()
    {
        if (GetPlayer() != null)
        {
            GetPlayer().GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
        }
    }
    public override void CloseVendor()
    {
        if (GetPlayer() != null)
        {
            GetPlayer().GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
            UI_Active = false;
            ToggleVendorUI(false);
        }
    }
    public override string Name => "Healer";
}
public class Blacksmith : NPC
{
    //Blacksmith will be used to upgrade Weapons and craft new ones 
    public override void VendorUI()
    {
       
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
    }
    public override void ToggleVendor()
    {
        if (GetPlayer() != null)
        {
            GetPlayer().GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
        }
    }
    public override void CloseVendor()
    {
        if (GetPlayer() != null)
        {
            GetPlayer().GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
            UI_Active = false;
            ToggleVendorUI(false);
        }
    }
    public override string Name => "BlackSmith";
}
public class Scribe : NPC
{
    //Scribe will let you unlock new areas and missions
    //possibly upgrade ur class abilities?
    public override void VendorUI()
    {
        //waluigi
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
    }
    public override void ToggleVendor()
    {
        if (GetPlayer() != null)
        {
            GetPlayer().GetComponent<GUIHolder>().GUI.SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
        }
    }
    public override void CloseVendor()
    {
        if (GetPlayer() != null)
        {
            GetPlayer().GetComponent<GUIHolder>().GUI.SetActive(false);
            UI_Active = false;
            ToggleVendorUI(false);
        }
    }
    public override string Name => "Scribe";
}
