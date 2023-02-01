using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquip
{

    //Called Upon Equip
    public void Enter(GameObject requester);

    //Called upon Damage Calculation
    public int Execute(GameObject requester, int damageAmount);

    //Called Upon Unequip
    public void Exit(GameObject requester);

}

public enum EquipType { ARMOR, GUNS, AUGMENTS }

public class Equipment : Item, IEquip
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

   public override void Enter(GameObject requester)
    {

    }

    public override int Execute(GameObject requester, int damageAmount)
    {
        //Debug.Log("Damage is NOT being done");
        //damageAmount = 0;
        //Decrease Damage From Slimes by 20%

        if (requester.GetComponent<Slime>() != null)
        {
            // damageAmount = (damageAmount / 100) * 80;
        }
        else
        {
            return base.Execute(requester, damageAmount);
        }
        return damageAmount;
    }
    public override void Exit(GameObject requester)
    {
       
    }
}

public class BomberArmor : Equipment
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
public class TorterraArmor : Equipment
{
    public EquipType Equip = EquipType.ARMOR;

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
public class WormArmor : Equipment
{
    public EquipType Equip = EquipType.ARMOR;

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
