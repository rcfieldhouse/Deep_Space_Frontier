using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine;

public class ServerInputManager : MonoBehaviour
{
    public PlayerActions currentAction;

    public enum PlayerActions
    {
        None,
        Revive,
        EquipCannon,
        ToggleNade,
        ToggleWeapon,
        //fuck ryan function names
        LooksieHere,
        Pause,
        ShootGun,
        Aim,
        ReleaseAim,
        ChamberGun,
        Reload,
        Jump,
        Interact,
        EndInteract,
        CookGrenade,
        ThrowGrenade,
        Ability,
        SprintTrue,
        SprintFalse,

        //TODO: Delete these for the final build
        GiveUp,
        GoInvincible,
        GoBoss,
    }

    // Use this for initialization
    void Start()
    {
        currentAction = PlayerActions.None;
    }

    public static void HandleKeyInput(int connectionID, PlayerActions action)
    {
        if (action == PlayerActions.None) return;

        PlayerData pData = ServerNetworkManager.playerList[connectionID];


        if (action == PlayerActions.Ability)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.Aim)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.ChamberGun)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.CookGrenade)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.EndInteract)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.EquipCannon)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.Interact)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.Jump)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.LooksieHere)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.Pause)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.ReleaseAim)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.Reload)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.Revive)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.ShootGun)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.SprintFalse)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.SprintTrue)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.ThrowGrenade)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.ToggleNade)
            pData.animations = PlayerAnimations.None;

        else if (action == PlayerActions.ToggleWeapon)
            pData.animations = PlayerAnimations.None;

        HandleMovement(connectionID, pData);
        MovementReconciliation(connectionID, pData);

        ServerNetworkSend.SendPlayerData(connectionID, pData);
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

