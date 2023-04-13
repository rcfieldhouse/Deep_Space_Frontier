using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using KaymakNetwork;

public class ClientPlayerInput : MonoBehaviour
{
    private float Sensitivity=1;
    Inputs PlayerInputController;
    public PlayerInput PlayerInput;
    Vector2 move, look;
    float Mouse = 0;
    bool UsingPrimary=true;
    // Start is called before the first frame update

    void Awake()
    {
        PlayerInputController = new Inputs();

        PlayerInputController.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        PlayerInputController.Player.Move.canceled += cntxt => move = Vector2.zero;

        PlayerInputController.Player.Look.performed += cntxt => look = cntxt.ReadValue<Vector2>();
        PlayerInputController.Player.Look.canceled += cntxt => look = Vector2.zero;
        PlayerInputController.Player.Look.performed += cntxt => LooksieHere(cntxt);
        PlayerInputController.Player.MouseScroll.performed += cntxt => Mouse = cntxt.ReadValue<float>();

        PlayerInputController.Player.Fire.performed += cntxt => ShootGun();
        PlayerInputController.Player.Fire.canceled += cntxt => ChamberGun();
        PlayerInputController.Player.ADS.performed += cntxt => Aim();
        PlayerInputController.Player.ADS.canceled += cntxt => ReleaseAim();
        PlayerInputController.Player.Grenade.performed += cntxt => CookGrenade();
        PlayerInputController.Player.Grenade.canceled += cntxt => ThrowGrenade();
        PlayerInputController.Player.Interact.performed += cntxt => Interact();
        PlayerInputController.Player.Interact.canceled += cntxt => EndInteract();
        PlayerInputController.Player.Sprinting.performed += cntxt => SprintTrue();
        PlayerInputController.Player.Sprinting.canceled += cntxt => SprintFalse();
        PlayerInputController.Player.Sprinting.started += cntxt => SprintTrue();

        PlayerInputController.Player.Reload.performed += cntxt => Reload();
        PlayerInputController.Player.Jump.performed += cntxt => Jump();
        PlayerInputController.Player.Ability.performed += cntxt => Ability();
        PlayerInputController.Player.Pause.performed += cntxt => Pause();
        PlayerInputController.Player.SwapWeapon.performed += cntxt => ToggleWeapon();
        PlayerInputController.Player.ToggleNade.performed += cntxt => ToggleNade();
        PlayerInputController.Player.ControllerSpecialWeapon.performed += cntxt => EquipCannon();
        PlayerInputController.Player.KeyboardSpecialWeapon.performed += cntxt => EquipCannon();

        PlayerInputController.Player.ReviveSelf.performed += cntxt => ReviveSelf();
        PlayerInputController.Player.GiveUp.performed += cntxt => GiveUp();
        PlayerInputController.Player.Invincibility.performed += cntxt => GoInvincible();
        PlayerInputController.Player.BossTeleport.performed += cntxt => GoBoss();
    }
    public void ReviveSelf()
    {
        PlayerInput.RevivePlayer();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Revive);
        }
    }
    public void GiveUp()
    {
        PlayerInput.GiveUpAndDie();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.GiveUp);
        }
    }
    public void GoInvincible()
    {
        PlayerInput.SetInvulnerable();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.GoInvincible);
        }
    }
    public void GoBoss()
    {
        PlayerInput.GoToBossArena();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.GoBoss);
        }
    }
    public void EquipCannon()
    {
        PlayerInput.SwapTertiaryWeapon();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.EquipCannon);
        }
    }
    public void ToggleNade()
    {
        PlayerInput.ToggleThrowable();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.ToggleNade);
        }
    }
    public void ToggleWeapon()
    {
        if (UsingPrimary)
            PlayerInput.SwapSecondaryWeapon();
        else PlayerInput.SwapPrimaryWeapon();

        UsingPrimary = !UsingPrimary;
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.ToggleWeapon);
        }
    }
    public void LooksieHere(InputAction.CallbackContext action)
    {
    
        string Device = action.control.device.ToString();
        if (Device == "Mouse:/Mouse")
            Sensitivity = 0.5f;
        else
            Sensitivity = 6;
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.LooksieHere);
        }
    }
    public void LooksieHere()
    {
       // Sensitivity = 8;
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.LooksieHere);
        }
    }
    public void Pause()
    {
        PlayerInput.PausePlayer();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Pause);
        }
    }
    public void ShootGun()
    {
        PlayerInput.ShootGun();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.ShootGun);
        }
    }
    public void Aim()
    {
        PlayerInput.Aim();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Aim);
        }
    }
    public void ReleaseAim()
    {
        PlayerInput.ReleaseAim();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.ReleaseAim);
        }
    }
    public void ChamberGun()
    {
        PlayerInput.ChamberGun();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.ChamberGun);
        }
    }
    public void Reload()
    {
        PlayerInput.PlayerReload();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Reload);
        }
    }
    public void Jump()
    {
        PlayerInput.Jump();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Jump);
        }
    }
    public void Interact()
    {
        PlayerInput.BeginInteract();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Interact);
        }
    }
    public void EndInteract()
    {
        PlayerInput.EndInteract();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.EndInteract);
        }
    }
    public void CookGrenade()
    {
         PlayerInput.CookGrenade();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.CookGrenade);
        }
    }
    public void ThrowGrenade()
    {
         PlayerInput.ReleaseGrenade();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.ThrowGrenade);
        }
    }
    public void Ability()
    {
        PlayerInput.UseClassAbility();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.Ability);
        }
    }
    public void SprintTrue()
    {
        Debug.Log("Sprinting");
        PlayerInput.SprintingTrue();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.SprintTrue);
        }
    }
    public void SprintFalse()
    {
        PlayerInput.SprintingFalse();
        if (!NetworkDriver.instance.isServer)
        {
            ClientNetworkSend.SendKeyInput(PlayerStates.SprintFalse);
        }
    }
    private void OnEnable()
    {
        PlayerInputController.Player.Enable();
      
    }
    private void OnDisable()
    {
        PlayerInputController.Player.Disable();
    }
    // Update is called once per frame
    void Update()
    {

        Vector2 vector;
        vector.x = look.x;
        vector.y = look.y;
        PlayerInput.LookInput(vector * Sensitivity);

        Vector2 vec;
        vec.x = move.x;
        vec.y = move.y;
        PlayerInput.MoveInput(vec);
       
        PlayerInput.MouseScrollInput(Mouse);
        Mouse = 0;

    }

    public void FixedUpdate()
    {
        if (!NetworkDriver.instance.isServer)
        {

            ClientNetworkSend.SendPlayerData();
        }
            
    }
}
