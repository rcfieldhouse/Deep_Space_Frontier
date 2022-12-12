using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    //using abstract class so that all vendors can derrive from this
    //also doing all of the like behaviours in the abstract so that they do not to be written twice
    private GameObject Prompt;
    public bool UI_Active;
    private void Awake()
    {
        UI_Active = false;
        Prompt = GameObject.Find("PickupPrompt");
        PlayerInput.Interact += VendorUI;
    }
    // Start is called before the first frame update
    public abstract void VendorUI();
    public abstract void VendorAction();
    public abstract string Name { get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Prompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Prompt.SetActive(false);
        }
    }
}




public class Merchant : NPC
{
    //merchant can be used to sell parts for currency
    public override void VendorUI()
    {
        UI_Active = !UI_Active;
        Debug.Log(UI_Active);
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
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
    public override string Name => "Scribe";
}
