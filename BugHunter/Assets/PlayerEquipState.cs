using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerEquipState
{
    public int DoState(GameObject DamageSource);
}

public class ArmorBase : PlayerEquipState
{
    public int DoState(GameObject DamageSource)
    {
        throw new System.NotImplementedException();
    }
}
