using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour


{
    //For the Teleport Command Pattern

    //actions that the player may perform
    public static Action JumpAction, UseAbility, Shoot, Chamber,Reload, Interact,Undo,TabThrowable, WeNeedToCookJesse;
    public static Action<bool>Crouching,ADS;
    public static Action<Quaternion> Look, Throw ;
    public static Action<Vector2,float> Move;

    //pause menu actions
    public static Action SavePlayer, LoadPlayer, PausePlugin, GetTime;



    //these are for weapon swapping,
    //swapping weapon is a placeholder until class selection is introduced

    public static Action SwapPrimary, SwapSecondary;
    public static Action<int> SwappingWeapon;
    private int WeaponActive = 0,WeaponListLength=5;

    //public static Action<bool,int>thing;
    [SerializeField] private float SpeedMod = 1.0f;
    [SerializeField] private CharacterController controller;
    [Range(0, 1)] [SerializeField] private float Sensitivity = .5f;

    private Quaternion Direction;    
    public Vector2 MouseInput;
    private Vector2 KeyboardInput;
    private bool UIToggle = true;
    private float MouseScroll=0.0f;

    public Vector3 Dir;
    public GameObject userInterface;
    // Start is called before the first frame update
    //damn you dante, make ur own file 
    void Start()
    {
        
        // Commented temporarily unitl inventory system is implemented
        Cursor.lockState= CursorLockMode.Locked;

        WeaponSwap.BroadcastWeaponListData += SetWeaponActive;
    }
 

    private void SetWeaponActive(int num,int length)
    {
        WeaponActive = num;
        WeaponListLength = length;
    }
    // Update is called once per frame
    void Update()
    {
        //MouseInput for aim
       MouseInput.x += Input.GetAxis("Mouse X") * Sensitivity * 2; 
       MouseInput.y += Input.GetAxis("Mouse Y") * Sensitivity * 2; 
        if (Mathf.Abs(MouseInput.y) > 90) {
            MouseInput.y-= MouseInput.y-(90* (MouseInput.y / Mathf.Abs(MouseInput.y)));
        }

       Direction = Quaternion.Euler(-MouseInput.y, MouseInput.x, 0);
        Dir.x = Direction.eulerAngles.x;
        MouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");


        KeyboardInput.x = Input.GetAxisRaw("Horizontal");
        KeyboardInput.y = Input.GetAxisRaw("Vertical");
        //PlayerINput for controls
        //Turn this to GetButtonDown at some point
        if (Input.GetKeyDown("space") && (controller.isGrounded() == true))
            JumpAction.Invoke(); 



        //sprint
       if (Input.GetKeyDown(KeyCode.LeftShift)) 
            SpeedMod = 1.5f;

       if (Input.GetKeyUp(KeyCode.LeftShift))
            SpeedMod = 1.0f;

        //SaveGamePlugin
        if (Input.GetKeyDown(KeyCode.P))
            SavePlayer.Invoke();

        //LoadGamePlugin
        if (Input.GetKeyDown(KeyCode.L))
            LoadPlayer.Invoke();

        //Pause Menu For Plugin
        if (Input.GetKeyDown(KeyCode.Escape))
            PausePlugin.Invoke();

        if (Input.GetKeyDown(KeyCode.T))
            GetTime.Invoke();

        if (Input.GetKeyDown(KeyCode.Z))
            Undo.Invoke();

        if (Input.GetKeyDown(KeyCode.G))
            TabThrowable.Invoke();

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




        //dodge mechanic
        if (Input.GetKeyDown(KeyCode.C))
            UseAbility.Invoke();
        // else if (Input.GetKeyUp(KeyCode.C))
        //     DodgeRoll.Invoke(false);



        //ability
        if (Input.GetKeyDown(KeyCode.Q))
            WeNeedToCookJesse.Invoke();

        if (Input.GetKeyUp(KeyCode.Q))
            Throw.Invoke(Direction);

        if (Input.GetKeyDown(KeyCode.R))
            Reload.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            Interact.Invoke();
        //shoot 
        //chamber is for full auto
        if (Input.GetButtonDown("Fire1"))
            Shoot.Invoke();
        else if (Input.GetButtonUp("Fire1"))
            Chamber.Invoke();
      

        //aim code
        if (Input.GetButtonDown("Fire2"))
        {
            UIToggle = !UIToggle;
            userInterface.SetActive(UIToggle);
            ADS.Invoke(true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            UIToggle = !UIToggle;
            userInterface.SetActive(UIToggle);
            ADS.Invoke(false);
        }

        //weapon swapping 
        //trying to find a better way 


        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwappingWeapon.Invoke(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwappingWeapon.Invoke(1);
       

        //change with scroll wheel
        if (MouseScroll > 0 &&WeaponActive>0)
            SwappingWeapon.Invoke(WeaponActive - 1);
        else if (MouseScroll < 0 && WeaponActive<WeaponListLength)
            SwappingWeapon.Invoke(WeaponActive + 1);




        Look.Invoke(Direction);
        Move.Invoke(KeyboardInput,SpeedMod);

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
