using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    //actions that the player may perform
    public static Action JumpAction, DodgeRoll, Shoot, Chamber;
    public static Action<bool>Crouching,ADS;
    public static Action<Quaternion> Look, UseAbility;
    public static Action<Vector2,float> Move;




    //public static Action<bool,int>thing;
    [SerializeField] private float SpeedMod = 1.0f;
    [SerializeField] private CharacterController controller;
    [Range(0, 1)] [SerializeField] private float Sensitivity = .5f;

    private Quaternion Direction;    
    private Vector2 MouseInput;
    private Vector2 KeyboardInput;
    private bool UIToggle = true;
    [SerializeField] private float MouseScroll=0.0f;

    public WeaponSwap weaponSwap;
    public GameObject userInterface;
    // Start is called before the first frame update
    //damn you dante, make ur own file 
    void Start()
    {

        // Commented temporarily unitl inventory system is implemented
        Cursor.lockState= CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //MouseInput for aim
       MouseInput.x += Input.GetAxis("Mouse X") * Sensitivity * 2; ;
       MouseInput.y += Input.GetAxis("Mouse Y") * Sensitivity * 2; ;
       Direction = Quaternion.Euler(-MouseInput.y, MouseInput.x, 0);

        MouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");


        KeyboardInput.x = Input.GetAxisRaw("Horizontal");
        KeyboardInput.y = Input.GetAxisRaw("Vertical");
        //PlayerINput for controls
        //Turn this to GetButtonDown at some point
        if (Input.GetKeyDown("space") && (controller.isGrounded() == true))
            JumpAction.Invoke(); 



        //sprint
       if (Input.GetKeyDown(KeyCode.LeftShift)) 
            SpeedMod = 2.0f;

       if (Input.GetKeyUp(KeyCode.LeftShift))
            SpeedMod = 1.0f;




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
            DodgeRoll.Invoke();
       // else if (Input.GetKeyUp(KeyCode.C))
       //     DodgeRoll.Invoke(false);



        //ability
        if (Input.GetKeyDown(KeyCode.Q))
            UseAbility.Invoke(Direction);
          
           
        

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
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            UIToggle = !UIToggle;
            userInterface.SetActive(UIToggle);
        }

            //weapon swapping 
           //try to find a better way 

        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponSwap.SetWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weaponSwap.SetWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            weaponSwap.SetWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            weaponSwap.SetWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            weaponSwap.SetWeapon(4);

        //change with scroll wheel
        if (MouseScroll > 0)
            weaponSwap.SetWeapon(weaponSwap.GetWeaponNum() - 1);
        else if (MouseScroll < 0)       
            weaponSwap.SetWeapon(weaponSwap.GetWeaponNum() + 1);
        



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
