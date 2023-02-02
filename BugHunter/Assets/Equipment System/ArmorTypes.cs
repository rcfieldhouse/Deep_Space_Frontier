using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, IEquip
{
    public EquipType equipType;
    public bool isEquippable = true;

    virtual public void Enter(GameObject requester)
    {
        //Change Player Skin
        //Update the player state
        //broadcast the event if applicable
    }

    virtual public int Execute(GameObject requester, int damageAmount)
    {
        Debug.Log("Damage is: " + damageAmount + " POST-mitigation, from " + requester.name);
        return damageAmount;
    }

    virtual public void Exit(GameObject requester)
    {
        //May not need to be implemented but here for future 
        //implementation should the need arise
    }
}

public class SlimeArmor : Armor
{
    public EquipType Equip = EquipType.ARMOR;

    public SlimeArmor()
    {
        this.itemName = "Slime Armor";
    }

    public override void Enter(GameObject requester)
    {

    }

    public override int Execute(GameObject requester, int damageAmount)
    {

        if (requester.GetComponent<Slime>() != null)
        {
            //Testing Functionality
            damageAmount = 0;
        }
        else
        {
            return base.Execute(requester, damageAmount);
        }
        Debug.Log("Damage is: " + damageAmount + " POST-mitigation, from " + requester.name);
        return damageAmount;
    }
    public override void Exit(GameObject requester)
    {

    }
}

public class BomberArmor : Armor
{
    public EquipType Equip = EquipType.ARMOR;

    public BomberArmor()
    {
        this.itemName = "Bomber Armor";
    }

    public override void Enter(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.JumpForce += new Vector3(0, 10, 0);
        base.Enter(requester);
    }

    public override void Exit(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.JumpForce -= new Vector3(0, 10, 0);
        base.Exit(requester);
    }

    //flat damage reduction from all sources
    public override int Execute(GameObject requester, int damageAmount)
    {
        damageAmount -= 10;
        Debug.Log("Damage is: " + damageAmount + " POST-mitigation, from " + requester.name);
        return base.Execute(requester, damageAmount);
    }
}

public class TorterraArmor : Armor
{
    public EquipType Equip = EquipType.ARMOR;
    public TorterraArmor()
    {
        this.itemName = "Torterra Armor";
    }

    public override void Enter(GameObject requester)
    {

    }
    public override int Execute(GameObject requester, int damageAmount)
    {
        return base.Execute(requester, damageAmount);
    }
    public override void Exit(GameObject requester)
    {

    }
}

public class WormArmor : Armor
{
    public EquipType Equip = EquipType.ARMOR;
    public WormArmor()
    {
        this.itemName = "Worm Armor";
    }

    public override void Enter(GameObject requester)
    {

    }
    public override int Execute(GameObject requester, int damageAmount)
    {
        return base.Execute(requester, damageAmount);
    }
    public override void Exit(GameObject requester)
    {

    }
}
