using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class ClientPlayerInput : NetworkBehaviour
{
    private float Sensitivity = 1;
    Inputs PlayerInputController;
    public PlayerInput PlayerInput;
    Vector2 move, look;
    float Mouse = 0;
    bool UsingPrimary = true;
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
        if (!IsOwner)
            return;
        PlayerInput.RevivePlayer();
    }
    public void GiveUp()
    {
        if (!IsOwner)
            return;
        PlayerInput.GiveUpAndDie();
    }
    public void GoInvincible()
    {
        if (!IsOwner)
            return;
        PlayerInput.SetInvulnerable();
    }
    public void GoBoss()
    {
        if (!IsOwner)
            return;
        PlayerInput.GoToBossArena();

    }
    public void EquipCannon()
    {
        if (!IsOwner)
            return;
        PlayerInput.SwapTertiaryWeapon();
    }
    public void ToggleNade()
    {
        if (!IsOwner)
            return;
        PlayerInput.ToggleThrowable();
    }
    public void ToggleWeapon()
    {
        if (!IsOwner)
            return;

        if (UsingPrimary)
            PlayerInput.SwapSecondaryWeapon();
        else PlayerInput.SwapPrimaryWeapon();

        UsingPrimary = !UsingPrimary;
    }
    public void LooksieHere(InputAction.CallbackContext action)
    {
        if (!IsOwner)
            return;

        string Device = action.control.device.ToString();
        if (Device == "Mouse:/Mouse")
            Sensitivity = 0.5f;
        else
            Sensitivity = 6;
    }
    public void LooksieHere()
    {
        // Sensitivity = 8;

    }
    public void Pause()
    {
        if (!IsOwner)
            return;

        PlayerInput.PausePlayer();

    }
    public void ShootGun()
    {
        if (!IsOwner)
            return;
        PlayerInput.ShootGun();
    }
    public void Aim()
    {
        if (!IsOwner)
            return;
        PlayerInput.Aim();
    }
    public void ReleaseAim()
    {
        if (!IsOwner)
            return;
        PlayerInput.ReleaseAim();
    }
    public void ChamberGun()
    {
        if (!IsOwner)
            return;
        PlayerInput.ChamberGun();
    }
    public void Reload()
    {
        if (!IsOwner)
            return;
        PlayerInput.PlayerReload();
    }
    public void Jump()
    {
        if (!IsOwner)
            return;
        PlayerInput.Jump();
    }
    public void Interact()
    {
        if (!IsOwner)
            return;
        PlayerInput.BeginInteract();
    }
    public void EndInteract()
    {
        if (!IsOwner)
            return;
        PlayerInput.EndInteract();
    }
    public void CookGrenade()
    {
        if (!IsOwner)
            return;
        PlayerInput.CookGrenade();
    }
    public void ThrowGrenade()
    {
        if (!IsOwner)
            return;
        PlayerInput.ReleaseGrenade();
    }
    public void Ability()
    {
        if (!IsOwner)
            return;
        PlayerInput.UseClassAbility();
    }
    public void SprintTrue()
    {
        if (!IsOwner)
            return;
        Debug.Log("Sprinting");
        PlayerInput.SprintingTrue();
    }
    public void SprintFalse()
    {
        if (!IsOwner)
            return;
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

   public override void OnNetworkSpawn()
   {
       if (!IsOwner)
       {
            gameObject.transform.parent.transform.GetChild(1).GetChild(3).GetChild(3).SetParent(transform);

            gameObject.transform.parent.transform.GetChild(1).gameObject.SetActive(false);           
            gameObject.transform.parent.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.parent.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.parent.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.parent.transform.GetChild(5).gameObject.SetActive(false);

            foreach (Transform child in transform)
            {
                child.gameObject.layer = 0;
            }

       }
   }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

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
}
