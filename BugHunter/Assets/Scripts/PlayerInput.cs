using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private bool Jump = false;
    [SerializeField] private float Sprint = 1.0f; 
    [SerializeField] private Quaternion Direction;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector2 MouseInput;
    [SerializeField] private Vector2 KeyboardInput;
    [Range(0, 1)][SerializeField] private float Sensitivity=.5f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       MouseInput.x += Input.GetAxis("Mouse X") * Sensitivity * 2; ;
       MouseInput.y += Input.GetAxis("Mouse Y") * Sensitivity * 2; ;
       Direction = Quaternion.Euler(-MouseInput.y, MouseInput.x, 0);


       if (Input.GetKeyDown("space") && (controller.isGrounded()==true))
            Jump = true; 

       if (Input.GetKeyDown(KeyCode.LeftShift)) 
            Sprint = 2.0f;

       if (Input.GetKeyUp(KeyCode.LeftShift))
            Sprint = 1.0f;

        KeyboardInput.x = Input.GetAxisRaw("Horizontal");
        KeyboardInput.y = Input.GetAxisRaw("Vertical");
    
        controller.Move(KeyboardInput, Jump, Sprint,Direction);
        Jump = false;
    }
}
