using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void VendorUI();
    public abstract void VendorAction();
    public abstract string Name { get; }
}
public class Merchant : NPC
{
    //merchant can be used to sell parts for currency
    public override void VendorUI()
    {
       //waluigi
    }
    public override void VendorAction()
    {
        throw new System.NotImplementedException();
    }
    public override string Name => "Merchant";
}
public class Healer : NPC
{
    //merchant can be used to sell parts for currency
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
    //merchant can be used to sell parts for currency
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
    //merchant can be used to sell parts for currency
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
