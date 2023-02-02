using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType { ARMOR, GUNS, AUGMENTS }
public interface IEquip
{
    //Called Upon Equip
    public void Enter(GameObject requester);

    //Called upon Damage Calculation
    public int Execute(GameObject requester, int damageAmount);

    //Called Upon Unequip
    public void Exit(GameObject requester);

}

