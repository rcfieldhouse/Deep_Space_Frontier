using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    FMODPlayer instance;


    public enum EquipType { Armor, Guns, Augments }

    public class Equipment : Item
    {
        public EquipType equipType;




    }


    public Equipment[] currentEquipment;
    public delegate void OnEquipmentChangedCallback();
    public OnEquipmentChangedCallback onEquipmentChangedCallback;

};