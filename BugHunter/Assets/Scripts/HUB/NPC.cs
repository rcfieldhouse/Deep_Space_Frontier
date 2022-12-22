using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class NPC : MonoBehaviour
{
    //using abstract class so that all vendors can derrive from this
    //also doing all of the like behaviours in the abstract so that they do not to be written twice
    private GameObject Prompt,Player;
    public bool UI_Active;
    public static Action<String,bool> SelectUI; 
    private void Awake()
    {
        UI_Active = false;
        Prompt = GameObject.Find("PickupPrompt");
        PlayerInput.Interact += OpenVendor;
    }

    public GameObject GetPlayer()
    {
        return Player;
    }
    public GameObject GetPrompt()
    {
        return Prompt;
    }
    // Start is called before the first frame update
    public abstract void VendorUI();
    public abstract void VendorAction();

    public abstract void OpenVendor();

    public void ToggleVendorUI(bool var)
    {
        SelectUI.Invoke(Name,var);
    }
    public void ToggleAimOnPlayer(bool var)
    {
        if (Player != null)
        {
            Player.transform.GetComponentInChildren<WeaponInfo>().SetCanShoot(!var);
            Player.transform.GetComponentInChildren<WeaponInfo>().SetIsReloading(var);
            Player.transform.GetComponentInChildren<Look>().SetIsPaused(var);
            if (var == true) Cursor.lockState = CursorLockMode.None;
            else if (var == false) Cursor.lockState = CursorLockMode.Locked;
        }
        

    }
    public abstract string Name { get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(Name);
            Player = other.transform.parent.gameObject;
            Prompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = null;
            Prompt.SetActive(false);
        }
    }
}




public class Merchant : NPC
{
    //merchant can be used to sell parts for currency
    public override void VendorUI()
    {
      
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
    }

    public override void OpenVendor()
    {
        if (GetPlayer() != null)
        {
            GetPrompt().SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
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
    public override void OpenVendor()
    {
        if (GetPlayer() != null)
        {
            GetPrompt().SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
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
    public override void OpenVendor()
    {
        if (GetPlayer() != null)
        {
            GetPrompt().SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
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
    public override void OpenVendor()
    {
        if (GetPlayer() != null)
        {
            GetPrompt().SetActive(false);
            UI_Active = !UI_Active;
            ToggleAimOnPlayer(UI_Active);
            ToggleVendorUI(UI_Active);
        }
    }
    public override string Name => "Scribe";
}
