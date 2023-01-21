using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerEquipState
{
    public int DoState(GameObject damageSource);
}

public class SlimeArmor : PlayerEquipState
{
    public void Start()
    {
        //playerInventory = GameObject.Find("mixamoCharacter").GetComponent<LootHolder>();
    }
    public int DoState(GameObject damageSource)
    {
        if (damageSource.name == "Zephyr")
        {
            return 10;
        }
        else 
            return 0;
    }
}
