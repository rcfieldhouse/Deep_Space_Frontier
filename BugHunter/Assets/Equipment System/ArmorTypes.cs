using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, IEquip
{
    public EquipType equipType;
    public bool isEquippable = false;
    virtual public void Enter(GameObject requester)
    {
        //Change Player Skin
        //Update the player state
        //broadcast the event if applicable
    }

    virtual public int Execute(GameObject requester, int damageAmount)
    {
        //Debug.Log("Damage is: " + damageAmount + " POST-mitigation, from " + requester.name);
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
        base.Enter(requester);
    }

    public override int Execute(GameObject requester, int damageAmount)
    {
        double temp = damageAmount;
        temp *= 0.9;
        damageAmount = (int)temp;

        //Debug.Log("Damage is: " + damageAmount + " POST-mitigation, from " + requester.name);
        return damageAmount;
    }
    public override void Exit(GameObject requester)
    {
        base.Exit(requester);
    }
}

public class StandardArmor : Armor
{


    public EquipType Equip = EquipType.ARMOR;

    public StandardArmor()
    {
        this.itemName = "Standard Armor";
        this.isEquippable = true;
    }

    public override void Enter(GameObject requester)
    {
        base.Enter(requester);
    }

    public override int Execute(GameObject requester, int damageAmount)
    {
            return base.Execute(requester, damageAmount);
    }
    public override void Exit(GameObject requester)
    {
        base.Exit(requester);
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
        GrenadeManager GrenadeHolder = requester.GetComponent<GrenadeManager>();

        GrenadeHolder.maxGrenades +=2;
        base.Enter(requester);
    }

    public override void Exit(GameObject requester)
    {
        GrenadeManager GrenadeHolder = requester.GetComponent<GrenadeManager>();
        GrenadeHolder.maxGrenades -= 2;
        base.Exit(requester);
    }

    //flat damage reduction from all sources
    public override int Execute(GameObject requester, int damageAmount)
    {
        Debug.Log("Damage is: " + damageAmount + " POST-mitigation, from " + requester.name);
        return base.Execute(requester, damageAmount);
    }
}

public class TickArmor : Armor
{
    public EquipType Equip = EquipType.ARMOR;
    public TickArmor()
    {
        this.itemName = "Tick Armor";
    }

    public override void Enter(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.ArmourSpeedIncrease = 1.10f;
        //TODO: Make this work for playerSpeed
       // playerControl.movespeed;
    }
    public override int Execute(GameObject requester, int damageAmount)
    {
        double temp = damageAmount;
        temp *= 0.95;
        damageAmount = (int)temp;
        return base.Execute(requester, damageAmount);
    }
    public override void Exit(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.ArmourSpeedIncrease = 1.0f;
    }
}

public class ZephyrArmor : Armor
{
    public EquipType Equip = EquipType.ARMOR;
    public ZephyrArmor()
    {
        this.itemName = "Zephyr Armor";
    }

    public override void Enter(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.ArmourSpeedIncrease = 3.30f;
    }
    public override int Execute(GameObject requester, int damageAmount)
    {
        return base.Execute(requester, damageAmount);
    }
    public override void Exit(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.ArmourSpeedIncrease = 1.0f;
    }
}
