using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour


{
    //For the Teleport Command Pattern

    //actions that the player may perform
    public event Action Interact, InteractReleased,Revive,GiveUp;
    public event Action JumpAction, UseAbility, Shoot, Chamber,Reload,Undo,TabThrowable, WeNeedToCookJesse;
    public event Action<bool>Crouching,ADS,Sprinting;
    public event Action<Quaternion> Look, Throw ;
    public event Action<Vector2,float> Move = delegate { };
    public bool IsDead = false;
    //pause menu actions
    public static Action SavePlayer, LoadPlayer, PausePlugin, GetTime;



    //these are for weapon swapping,
    //swapping weapon is a placeholder until class selection is introduced

    public event Action SwapPrimary, SwapSecondary;
    public event Action<int> SwappingWeapon = delegate { };
    private int WeaponActive = 0,WeaponListLength=5;

    //public static Action<bool,int>thing;
    [SerializeField] private float SpeedMod = 1.0f;
    [SerializeField] private CharacterController controller;
    [Range(0, 1)] [SerializeField] private float Sensitivity = .5f;
    [Range(0, 1)] [SerializeField] private float SniperSensitivityReduction = 1.0f;
    [Range(0, 1)] [SerializeField] private float AimAssistStrength = 1.0f;
    [HideInInspector] public bool ADSWSniper = false,AimAssist = false;
    private Quaternion Direction;    
    public Vector2 MouseInput;
    private Vector2 KeyboardInput;
    private bool UIToggle = true;
    //public GameObject GenshinCam;
    private float MouseScroll=0.0f;

    public Vector3 Dir;
    public GameObject UserInterface;
    // Start is called before the first frame update
    //damn you dante, make ur own file 
    void Awake()
    {
        ADSWSniper = false;
        // Commented temporarily unitl inventory system is implemented
        Cursor.lockState= CursorLockMode.Locked;
        UserInterface = GetComponent<GUIHolder>().GUI;
        WeaponSwap.BroadcastWeaponListData += SetWeaponActive;
    }
    public void SetIsDead(bool var)
    {
        IsDead = var;
    }
    public void AutomaticBandAid()
    {
        if (IsDead == true)
            return;
        //had to use this so that i could invoke the full auto shoot 
        Shoot.Invoke();
    }
    private void OnDestroy()
    {
        WeaponSwap.BroadcastWeaponListData -= SetWeaponActive;
    }
    private void SetWeaponActive(int num,int length)
    {
        WeaponActive = num;
        WeaponListLength = length;
    }
    public void SetAimWSniper(bool var)
    {
        ADSWSniper = var;
    }
    public void SprintingFalse()
    {
        SpeedMod = 1.0f;
        Sprinting.Invoke(false);
    }
    public void SprintingTrue()
    {
        SpeedMod = 1.5f;
        Sprinting.Invoke(true);   
    }
    public void Jump()
    {
        JumpAction.Invoke();
    }
    public void ToggleThrowable()
    {
        TabThrowable.Invoke();
    }
    public void UseClassAbility()
    {
        UseAbility.Invoke();
    }
    public void CookGrenade()
    {
        WeNeedToCookJesse.Invoke();
    }

    public void ReleaseGrenade()
    {
        Throw.Invoke(Direction);
    }
    public void PausePlayer()
    {
            PausePlugin.Invoke();
    }
    public void BeginInteract()
    {
            Interact.Invoke();
    }
    public void EndInteract()
    {
            InteractReleased.Invoke();
    }
    public void PlayerReload()
    {
            Reload.Invoke();
    }
    public void ShootGun()
    {
        if (IsDead == false)
            Shoot.Invoke();     
    }
    public void ChamberGun()
    {
           if (IsDead == false)
            Chamber.Invoke();
    }
    public void Aim()
    {
        if (IsDead==false)
        {
            UIToggle = !UIToggle;
            UserInterface.SetActive(UIToggle);
            ADS.Invoke(true);
        }
    }
    public void ReleaseAim()
    {
        if (IsDead==false)
        {
            UIToggle = !UIToggle;
            UserInterface.SetActive(UIToggle);
            ADS.Invoke(false);
        }
    }
    public void SwapPrimaryWeapon()
    {
            SwappingWeapon.Invoke(0);
    }
    public void SwapSecondaryWeapon()
    {
            SwappingWeapon.Invoke(1);
    }
    public void LookInput(Vector2 LookInput)
    {

        MouseInput.x += LookInput.x * Sensitivity * 2;
        MouseInput.y += LookInput.y * Sensitivity * 2;
        if (ADSWSniper == true)
        {
            MouseInput.x -= (1.0f - SniperSensitivityReduction) * LookInput.x * Sensitivity * 2;
            MouseInput.y -= (1.0f - SniperSensitivityReduction) * LookInput.y * Sensitivity * 2;
        }
        if (AimAssist == true)
        {
            MouseInput.x -= (SniperSensitivityReduction * AimAssistStrength) * LookInput.x * Sensitivity * 2;
            MouseInput.y -= (SniperSensitivityReduction * AimAssistStrength) * LookInput.y * Sensitivity * 2;
        }

        if (Mathf.Abs(MouseInput.y) > 80)
        {
            MouseInput.y -= MouseInput.y - (80 * (MouseInput.y / Mathf.Abs(MouseInput.y)));
        }

        Direction = Quaternion.Euler(-MouseInput.y, MouseInput.x, 0);
        Dir.x = Direction.eulerAngles.x;
        if (Look != null && Direction != null)
        {
            Look.Invoke(Direction);
            Move.Invoke(KeyboardInput, SpeedMod);
        }
    }
    public void MoveInput(Vector2 LookInput)
    {
        KeyboardInput.x = LookInput.x;
        KeyboardInput.y = LookInput.y;
    }
        public void MouseScrollInput(float Scroll)
    {
        MouseScroll = Scroll;
        //change with scroll wheel
        if (MouseScroll > 0 && WeaponActive > 0)
            SwappingWeapon.Invoke(WeaponActive - 1);
        else if (MouseScroll < 0 && WeaponActive < WeaponListLength)
            SwappingWeapon.Invoke(WeaponActive + 1);
    }
    // Update is called once per frame
    void Update()
    {
     



      


   
        //PlayerINput for controls
        //Turn this to GetButtonDown at some point
      

        //DEVHACK
        if (Input.GetKeyDown(KeyCode.P))
            GetComponent<HealthSystem>().SetInvulnerable(true);
        if (Input.GetKeyDown(KeyCode.O))
            Revive.Invoke();
        if (Input.GetKeyDown(KeyCode.L))
            GiveUp.Invoke();

        //Pause Menu For Plugin
   

        if (Input.GetKeyDown(KeyCode.T))
            GetTime.Invoke();

        if (Input.GetKeyDown(KeyCode.Z))
            Undo.Invoke();

        if (Input.GetKeyDown(KeyCode.F))
        {
        //    GenshinCam.SetActive(!GenshinCam.activeInHierarchy);
        }
            

        
        if (Input.GetKeyDown(KeyCode.RightArrow))
            GameManager.instance.ResumeTime();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GameManager.instance.StopTime();


        //cursed crouch controls
        if (Input.GetButtonDown("Crouch"))
            Crouching.Invoke(true);      
        else if (Input.GetButtonUp("Crouch"))
            Crouching.Invoke(false);
     
        if (Input.GetButtonDown("Crouch") && (SpeedMod != 2.0f))
            SpeedMod = controller.m_CrouchSpeed;
        if (Input.GetButtonDown("Crouch") && (SpeedMod == 2.0f))
            SpeedMod = 2.0f;
        else if (Input.GetButtonUp("Crouch"))
            SpeedMod = 1.0f;


   


      

    
      

    }
    // UI buttons call this when they want to enable mouse lock
    // Currently used by "Exit_Inventory" Button
    public void MouseState()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
