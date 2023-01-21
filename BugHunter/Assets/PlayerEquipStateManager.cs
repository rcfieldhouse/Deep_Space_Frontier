using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipStateManager
{
    PlayerEquipState CurrentState;

    public void ExecuteState(GameObject damageSource)
    {
        CurrentState.DoState(damageSource);
    }

    public void SetState(PlayerEquipState newState)
    {
        CurrentState = newState;
    }

    public PlayerEquipState GetState()
    {
        return CurrentState;
    }
}
