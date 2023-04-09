using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: Make a Weapon/Augment Only version of the manager
/// perhaps integrate with current gun system? 
/// </summary>
/// 
public enum Armors
{
    None,
    SlimeArmor,
    BomberArmor,
    TickArmor,
    ZephyrArmor
}
public class EquipmentManager : MonoBehaviour, IDataPersistence
{
    public Armor currentEquip;

    public SlimeArmor slime_armor;
    public BomberArmor BomberArmor;
    public TickArmor TickArmor;
    public ZephyrArmor ZephyrArmor;

    public bool ValidateEquip(Armor equip)
    {
        return equip.isEquippable;
    }

    private void Awake()
    {
        if(GameObject.Find("SceneLoadData") != null)
            Debug.Log("Equipment manager is awake");

        //temporary for testing
        slime_armor = new SlimeArmor();
        BomberArmor = new BomberArmor();
        TickArmor = new TickArmor();
        ZephyrArmor = new ZephyrArmor();


        currentEquip = new StandardArmor();
        //currentEquip.isEquippable = true;
        //ChangeEquip(currentEquip);
        // LoadData(GameObject.Find("SceneLoadData").GetComponent<SceneLoadData>().data);
        LoadData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
    }
    
    public void ChangeEquip(Armor newEquip)
    {
        Debug.Log("I have equipped " + newEquip.itemName);

        //will need some UI for this, debug.logs for now though.
        if (!ValidateEquip(newEquip))
            DisplayCannotEquipError();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/EquipItem");

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
    private void OnDestroy()
    {
        //SaveData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
    }
    public void LoadData(GameData data)
    {
        Debug.Log("Equipment manager is loading");
        if (data.CurrentArmor == 0)
        {
            currentEquip = new StandardArmor();
        }

        else if (data.CurrentArmor == 1)
        { 
            currentEquip = slime_armor;
        }

        else if (data.CurrentArmor == 2)
        { 
            currentEquip = BomberArmor;
        }

        else if (data.CurrentArmor == 3)
        { 
            currentEquip = TickArmor;
        }

        else if (data.CurrentArmor == 4)
        { 
            currentEquip = ZephyrArmor;
        }

        ChangeEquip(currentEquip);
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Equipment manager is saving");
        if (currentEquip.itemName == "Standard Armor")
            data.CurrentArmor = 0;

        else if (currentEquip.itemName == "Slime Armor")
            data.CurrentArmor = 1;

        else if (currentEquip.itemName == "Bomber Armor")
            data.CurrentArmor = 2;

        else if (currentEquip.itemName == "Tick Armor")
            data.CurrentArmor = 3;

        else if (currentEquip.itemName == "Zephyr Armor")
            data.CurrentArmor = 4;
    }
}