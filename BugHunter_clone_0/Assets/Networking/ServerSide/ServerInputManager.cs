using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine;

public class ServerInputManager : MonoBehaviour
{
    public PlayerStates currentAction;
    // Use this for initialization
    void Start()
    {
        currentAction = PlayerStates.None;
    }

    

    private static void HandleMovement(int connectionID, PlayerData pData)
    {
        //PerformMovementCheck(int connectionID, pData);
    }

    private static void MovementReconciliation(int connectionID, PlayerData pData)
    {
        //ReconcileMovement(int connectionID, pData);
    }
}

