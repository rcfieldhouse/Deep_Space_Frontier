using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
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
    void ReviveSelf()
    {
        PlayerInput.RevivePlayer();
    }
    void GiveUp()
    {
        PlayerInput.GiveUpAndDie();
    }
    void GoInvincible()
    {
        PlayerInput.SetInvulnerable();
    }
    void GoBoss()
    {
        PlayerInput.GoToBossArena();
    }
    void EquipCannon()
    {
        PlayerInput.SwapTertiaryWeapon();
    }
    void ToggleNade()
    {
           PlayerInput.ToggleThrowable();
    }
    void ToggleWeapon()
    {
        if (UsingPrimary)
            PlayerInput.SwapSecondaryWeapon();
        else PlayerInput.SwapPrimaryWeapon();

        UsingPrimary = !UsingPrimary;
    }
    void LooksieHere(InputAction.CallbackContext action)
    {
        string Device = action.control.device.ToString();
        if (Device == "Mouse:/Mouse")
            Sensitivity = 0.5f;
        else
            Sensitivity = 8;
    }
    private void Pause()
    {
        PlayerInput.PausePlayer();
    }
    void ShootGun()
    {
        PlayerInput.ShootGun();
    }
    void Aim()
    {
        PlayerInput.Aim();
    }
    void ReleaseAim()
    {
        PlayerInput.ReleaseAim();
    }
    void ChamberGun()
    {
        PlayerInput.ChamberGun();
    }
    void Reload()
    {
        PlayerInput.PlayerReload();
    }
    void Jump()
    {
        PlayerInput.Jump();
    }
    void Interact()
    {
        PlayerInput.BeginInteract();
    }
    void EndInteract()
    {
        PlayerInput.EndInteract();
    }
    void CookGrenade()
    {
         PlayerInput.CookGrenade();
    }
    void ThrowGrenade()
    {
         PlayerInput.ReleaseGrenade();
    }
    void Ability()
    {
        PlayerInput.UseClassAbility();
    }
    void SprintTrue()
    {
        Debug.Log("Sprinting");
        PlayerInput.SprintingTrue();
    }
    void SprintFalse()
    {
        PlayerInput.SprintingFalse();
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
     PlayerInput.LookInput(vector*Sensitivity);
     Vector2 vec;
     vec.x = move.x;
     vec.y = move.y;
     PlayerInput.MoveInput(vec);

     PlayerInput.MouseScrollInput(Mouse);
     Mouse = 0; 
    
     // if (Input.GetKeyDown(KeyCode.O))
     //    
     // if (Input.GetKeyDown(KeyCode.L))
     //     

    }
}
