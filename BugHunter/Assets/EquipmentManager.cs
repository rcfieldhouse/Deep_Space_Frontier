using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    
    private Equipment currentEquip;

    SlimeArmor slime_armor = new SlimeArmor();
    BomberArmor jump_Armor = new BomberArmor();
    TorterraArmor torterra_Armor = new TorterraArmor();
    WormArmor worm_Armor = new WormArmor();

    public bool ValidateEquip(Equipment equip)
    {
        return equip.isEquippable;
    }

    private void Awake()
    {   
        //temporary for testing
        ChangeEquip(currentEquip);
    }

    public void ChangeEquip(Equipment newEquip)
    {
        Debug.Log("I have equipped");

        if(ValidateEquip(newEquip))
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
        else
            return damageAmount;
    }

}