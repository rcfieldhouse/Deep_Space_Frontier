using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public GameObject CameraMain,CameraCrouch,CameraDodge, CameraManager;
 
    [SerializeField] private LayerMask m_WhatIsGround;

    [Range(0, 1)][SerializeField] public float m_CrouchSpeed = 0.5f;
    [Range(0, 1)][SerializeField] private float SpeedSlider = .5f;

    private WaitForSeconds RollTimer = new WaitForSeconds(1.0f);

    [SerializeField] private Vector3 mover,JumpForce;
     private bool m_Grounded = true;
    [SerializeField] private CapsuleCollider coll;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PlayerInput.DodgeRoll += SwitchDodgeCam;
        PlayerInput.DodgeRoll += Dodge;
        PlayerInput.Crouching += SwitchCamCrouch;
        PlayerInput.JumpAction += Jump;
      //  PlayerInput.Look += Aim;
        PlayerInput.Move += Move;
        //this line sets default cam to main
        disableCams(false);
        
    }

    //functions for delegates 
    //happy programmer noises ;)

    private void disableCams(bool var)
    {   //set all cameras to false, if called by a function that is setting that camera to false
        //set default cam to main
        CameraCrouch.SetActive(false);
        CameraDodge.SetActive(false);
        CameraMain.SetActive(!var);
    }
  
    private void SwitchDodgeCam()
    {
        disableCams(true);
        CameraDodge.SetActive(true);
        StartCoroutine(RollTime());
   
    }
    private void SwitchLadderCam()
    { 
        disableCams(true);
        CameraDodge.SetActive(true);
    }
    private IEnumerator RollTime()
    {
        yield return RollTimer;
        disableCams(false);
  
    }

    private void SwitchCamCrouch(bool var)
    {
        disableCams(var);
        CameraCrouch.SetActive(var);       
    }
    private void Jump()
    {
        Rigidbody.velocity += JumpForce;
    }

    private void Aim(Quaternion quaternion)
    {
        Rigidbody.gameObject.transform.localRotation = quaternion;
    }
    private void Move(Vector2 move, float SpeedMod)
    {
        SpeedMod *= 5;
        mover = transform.right * move.x + transform.forward * move.y;
        Rigidbody.velocity = new Vector3(mover.x * SpeedSlider * SpeedMod, Rigidbody.velocity.y, mover.z * SpeedSlider * SpeedMod);
        Rigidbody.angularVelocity = Vector3.zero;
    }
    
    private void Dodge()
    {

    }
    // Update is called once per frame
    void Update()
    {
      
        gameObject.transform.rotation = Quaternion.Euler(0.0f, CameraManager.transform.eulerAngles.y, 0.0f);
     //  gameObject.transform.rotation = CameraManager.transform.rotation;


    }


    public bool isGrounded()
    {
        m_Grounded = Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down, 1.2f, m_WhatIsGround);
        return Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down, 1.2f, m_WhatIsGround);
    }
}
