using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class ClientPlayerInput : MonoBehaviour
{

	public PlayerInput playerInput;

	public void Start()
	{
		playerInput = GetComponent<PlayerInput>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2	vector;
		//vector.x = Input.GetAxis("Mouse X");
		//vector.y = Input.GetAxis("Mouse Y");
		//playerInput.LookInput(vector);
		Vector2 vec;
		//vec.x = Input.GetAxisRaw("Horizontal");
		//vec.y = Input.GetAxisRaw("Vertical");
		//playerInput.MoveInput(vec);

		playerInput.MouseScrollInput(Input.GetAxisRaw("Mouse ScrollWheel"));

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
            Sensitivity = 12;
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

		if (Input.GetKeyUp(KeyCode.LeftShift))
			playerInput.SprintingFalse(); 
		if (Input.GetKeyDown(KeyCode.LeftShift))
			playerInput.SprintingTrue();     
		if (Input.GetKeyDown("space"))
			playerInput.Jump();
		if (Input.GetKeyDown(KeyCode.G))
			playerInput.ToggleThrowable();
		if (Input.GetKeyDown(KeyCode.C))
			playerInput.UseClassAbility();
		if (Input.GetKeyDown(KeyCode.Q))
			playerInput.CookGrenade();
		if (Input.GetKeyUp(KeyCode.Q))
			playerInput.ReleaseGrenade();
		if (Input.GetKeyDown(KeyCode.Escape))
			playerInput.PausePlayer();
		if (Input.GetKeyDown(KeyCode.E))
			playerInput.BeginInteract();
		if (Input.GetKeyUp(KeyCode.E))
			playerInput.EndInteract();
		if (Input.GetKeyDown(KeyCode.R))
			playerInput.PlayerReload();
		if (Input.GetButtonDown("Fire1"))
			playerInput.ShootGun();
		if (Input.GetButtonUp("Fire1"))
			playerInput.ChamberGun();
		if (Input.GetButtonDown("Fire2"))
			playerInput.Aim();
		if (Input.GetButtonUp("Fire2"))
			playerInput.ReleaseAim();
		if (Input.GetKeyDown(KeyCode.Alpha1))
			playerInput.SwapPrimaryWeapon();
		if (Input.GetKeyDown(KeyCode.Alpha2))
			playerInput.SwapSecondaryWeapon();
		if (Input.GetKeyDown(KeyCode.O))
			playerInput.RevivePlayer();
		if (Input.GetKeyDown(KeyCode.L))
			playerInput.GiveUpAndDie();

	}
}
