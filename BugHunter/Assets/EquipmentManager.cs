using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: Make a Weapon/Augment Only version of the manager
/// perhaps integrate with current gun system? 
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    public Armor currentEquip;

    public bool ValidateEquip(Armor equip)
    {
        return equip.isEquippable;
    }

    private void Start()
    {
        //temporary for testing
        SlimeArmor slime_armor = new SlimeArmor();
        BomberArmor jump_Armor = new BomberArmor();
        TorterraArmor torterra_Armor = new TorterraArmor();
        WormArmor worm_Armor = new WormArmor();

        currentEquip = slime_armor;
        ChangeEquip(currentEquip);
    }

    public void ChangeEquip(Armor newEquip)
    {
        Debug.Log("I have equipped "+ newEquip.itemName);

        //will need some UI for this, debug.logs for now though.
        if (!ValidateEquip(newEquip))
            DisplayCannotEquipError();

        if (currentEquip != null)
        {
            currentEquip.Exit(transform.gameObject);
        }
        currentEquip = newEquip;
        currentEquip.Enter(transform.gameObject);
    }

    public int ExecuteEquip(GameObject requester, int damageAmount)
    {
        if (currentEquip != null)
        {   
            return currentEquip.Execute(requester, damageAmount);
        }
            Debug.LogError("Current Equip was not found! Damage returned to default settings.");
            return damageAmount;          
    }

    public void DisplayCannotEquipError()
    {
        Debug.LogError("Cannot Equip this right now!");
    }

}