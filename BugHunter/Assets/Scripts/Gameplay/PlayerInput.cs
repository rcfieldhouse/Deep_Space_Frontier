using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    //For the Teleport Command Pattern
    //actions that the player may perform

    public event Action Interact, InteractReleased,Revive,GiveUp;
    public event Action JumpAction, UseAbility, Shoot, Chamber,Reload,Undo,TabThrowable, WeNeedToCookJesse, PausePlugin;
    public event Action<bool>Crouching,ADS,Sprinting;
    public event Action<Quaternion> Look, Throw ;
    public event Action<Vector2,float> Move = delegate { };
    public bool IsDead = false;

    //pause menu actions
    //public static Action SavePlayer, LoadPlayer, GetTime;

    //these are for weapon swapping,
    //swapping weapon is a placeholder until class selection is introduced

   // public event Action SwapPrimary, SwapSecondary;
    public event Action<int> SwappingWeapon = delegate { };
    private int WeaponActive = 0,WeaponListLength=5;

    //public static Action<bool,int>thing;
    [SerializeField] private float SpeedMod = 1.0f;
    [SerializeField] private CharacterController controller;
    [Range(0, 1)]public float Sensitivity = .5f;
    [Range(0, 1)]public float SniperSensitivityReduction = 1.0f;
    [Range(0, 1)]public float AimAssistStrength = 1.0f;
    [Range(0, 1)] public float ADSAimStrength = 1.0f;
    [HideInInspector] public bool ADSWSniper = false, Aiming =false,AimAssist = false;
    private Quaternion Direction;    
    public Vector2 MouseInput;
    private Vector2 KeyboardInput;
    private bool UIToggle = true;
    //public GameObject GenshinCam;
    private float MouseScroll=0.0f;

    public Vector3 Dir;
    public GameObject UserInterface;
    public WeaponSwap WeaponSwap;

    public TutorialController TutorialInput;

    void Awake()
    {
        ADSWSniper = false;
        Cursor.lockState = CursorLockMode.Locked;
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
    public void SetAimWRegular(bool var)
    {
        Aiming = var;
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

        if (TutorialInput != null)
            TutorialInput.LSheftKeyDown();
    }
    public void Jump()
    {
        JumpAction.Invoke();

        if (TutorialInput != null)
            TutorialInput.SpacebarKeyDown();
    }
    public void ToggleThrowable()
    {
        TabThrowable.Invoke();
    }
    public void UseClassAbility()
    {
        UseAbility.Invoke();

        if (TutorialInput != null)
            TutorialInput.CKeyDown();
    }
    public void CookGrenade()
    {
        WeNeedToCookJesse.Invoke();
    }

    public void ReleaseGrenade()
    {
        Throw.Invoke(Direction);

        if (TutorialInput != null)
            TutorialInput.QKeyDown();
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

        if (TutorialInput != null)
            TutorialInput.RKeyDown();
    }
    public void ShootGun()
    {
        if (IsDead == false)
            Shoot.Invoke();

        if (TutorialInput != null)
            TutorialInput.LeftMouseDown();
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

        if(TutorialInput!=null)
        TutorialInput.RightMouseDown();
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
    public void SwapTertiaryWeapon()
    {
        SwappingWeapon.Invoke(2);
    }
    public void RevivePlayer()
    {
        Revive.Invoke();
    }
    public void GiveUpAndDie()
    {
        GiveUp.Invoke();
    }
    public void LookInput(Vector2 LookInput)
    {
        if (IsDead)
            return;

        MouseInput.x += LookInput.x * Sensitivity ;
        MouseInput.y += LookInput.y * Sensitivity ;
        if (ADSWSniper == true)
        {
            MouseInput.x -= (1.0f - SniperSensitivityReduction) * LookInput.x * Sensitivity ;
            MouseInput.y -= (1.0f - SniperSensitivityReduction) * LookInput.y * Sensitivity ;
        }
        if (Aiming == true)
        {
            MouseInput.x -= (1.0f - ADSAimStrength) * LookInput.x * Sensitivity;
            MouseInput.y -= (1.0f - ADSAimStrength) * LookInput.y * Sensitivity;
        }
        if (AimAssist == true)
        {
            MouseInput.x -= (SniperSensitivityReduction * AimAssistStrength) * LookInput.x * Sensitivity;
            MouseInput.y -= (SniperSensitivityReduction * AimAssistStrength) * LookInput.y * Sensitivity;
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
    public void MoveInput(Vector2 MoveInput)
    {    
        KeyboardInput.x = MoveInput.x;
        KeyboardInput.y = MoveInput.y;
        if (TutorialInput != null && MoveInput != Vector2.zero)
            TutorialInput.MoveKeyDown();
    }
    public void MouseScrollInput(float Scroll)
    {
        MouseScroll = Scroll;
        //change with scroll wheel
        if (MouseScroll > 0 && WeaponActive > 0)
            SwappingWeapon.Invoke(WeaponActive - 1);
        else if (MouseScroll < 0 && WeaponActive < WeaponListLength)
        {
            SwappingWeapon.Invoke(WeaponActive + 1);
            if (TutorialInput != null)
            {
                TutorialInput.ScrollWheelDown();
            }
        }
    }
    public void SetInvulnerable()
    {
         GetComponent<HealthSystem>().SetInvulnerable(true);
    }
   public void GoToBossArena()
    {
         GetComponent<Rigidbody>().position = new Vector3(170.0f, 25.0f, 420.0f);
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
