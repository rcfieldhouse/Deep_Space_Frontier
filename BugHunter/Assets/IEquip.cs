using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquip
{
    //CalledUpon Equip
    public void Enter(GameObject requester);

    //Called upon Damage Calculation
    public int Execute(GameObject requester, int damageAmount);

    //CalledUpon Unequip
    public void Exit(GameObject requester);

}

public enum EquipType { ARMOR, GUNS, AUGMENTS }

public class Equipment : Item, IEquip
{
    public EquipType equipType;

    virtual public void Enter(GameObject requester)
    {
        //Change Player Skin
        //Update the player state
        //broadcast the event if applicable
    }

    virtual public int Execute(GameObject requester, int damageAmount)
    {
        return damageAmount;
    }

    virtual public void Exit(GameObject requester)
    {
        //May not need to be implemented but here for future 
        //implementation should the need arise
    }
}



public class SlimeArmor : Equipment
{
    public EquipType Equip = EquipType.ARMOR;
    public override int Execute(GameObject requester, int damageAmount)
    {
        //Decrease Damage From Slimes by 20%
        if (requester.GetComponent<Slime>() != null)
        {
            damageAmount = (damageAmount / 100) * 20;
            return damageAmount;
        }
        else
        {
            return base.Execute(requester, damageAmount);
        }
            
    }
}

public class JumpArmor : Equipment
{
    public EquipType Equip = EquipType.ARMOR;

    public override void Enter(GameObject requester)
    {
        CharacterController playerControl = requester.GetComponent<CharacterController>();
        playerControl.JumpForce += new Vector3(0,10,0);
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
            return base.Execute(requester, damageAmount);      
    }
}
