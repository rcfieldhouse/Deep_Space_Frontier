using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private bool Jump = false, Crouch = false;

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
    public GrenadeThrow _GrenadeThrow; 
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

      
        //PlayerINput for controls
        //Turn this to GetButtonDown at some point
       if (Input.GetKeyDown("space") && (controller.isGrounded()==true))
            Jump = true; 

       if (Input.GetKeyDown(KeyCode.LeftShift)) 
            SpeedMod = 2.0f;

       if (Input.GetKeyUp(KeyCode.LeftShift))
            SpeedMod = 1.0f;

        //cursed crouch controls
        if (Input.GetButtonDown("Crouch"))
            Crouch = true;
        else if (Input.GetButtonUp("Crouch")) 
            Crouch= false;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(_GrenadeThrow.ThowGrenade(Direction * (Vector3.forward * 15+Vector3.up*5)));
           
        }
          


        if (Input.GetButtonDown("Crouch") && (SpeedMod != 2.0f))
            SpeedMod = controller.m_CrouchSpeed;
        if (Input.GetButtonDown("Crouch") && (SpeedMod == 2.0f))
            SpeedMod = 2.0f;
        else if (Input.GetButtonUp("Crouch"))
            SpeedMod = 1.0f;


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
            MouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponSwap.SetWeapon(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            weaponSwap.SetWeapon(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            weaponSwap.SetWeapon(2);

        if (MouseScroll > 0)
        {
            weaponSwap.SetWeapon(weaponSwap.GetWeaponNum() - 1);
        }
        else if (MouseScroll < 0)
        {
            weaponSwap.SetWeapon(weaponSwap.GetWeaponNum() + 1);
        }


        KeyboardInput.x = Input.GetAxisRaw("Horizontal");
        KeyboardInput.y = Input.GetAxisRaw("Vertical");
    
        controller.Move(KeyboardInput, Jump, SpeedMod,Direction, Crouch);
        Jump = false;
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
