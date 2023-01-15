using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public GameObject CameraMain,CameraCrouch,CameraDodge, CameraManager,WeaponCamera;
    private bool SuspendMovement = false, OnZipline=false;
    private Transform StartPos, EndPos;
    bool _IsOnLadder = false;
    [SerializeField] private LayerMask m_WhatIsGround;

    [Range(0, 1)][SerializeField] public float m_CrouchSpeed = 0.5f;
    [Range(0, 1)] [SerializeField] public float m_LadderSpeed = 0.5f;
    [Range(0, 1)][SerializeField] private float SpeedSlider = .5f;
  
    public WaitForSeconds RollTimer = new WaitForSeconds(0.75f);

    [SerializeField] private Vector3 mover,JumpForce = new Vector3 (0.0f,25.0f,0.0f);
     private bool m_Grounded = true;
    [SerializeField] private CapsuleCollider coll;
    //Used for initial save when player starts the game
    private bool MovedOnce = false;
    // Start is called before the first frame update
    private void Start()
    {
        disableCams(false);
        CameraMain.SetActive(true);
    }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        // PlayerInput.UseAbility += Dodge;
        PlayerInput.Crouching += SwitchCamCrouch;
        PlayerInput.JumpAction += Jump;
        PlayerInput.Move += Move;
        coll = GetComponent<CapsuleCollider>();
        //this line sets default cam to main
 
        disableCams(false);
        CameraMain.SetActive(true);

    }
    private void OnDestroy()
    {
        PlayerInput.Crouching -= SwitchCamCrouch;
        PlayerInput.JumpAction -= Jump;
        PlayerInput.Move -= Move;
    }
    public void SetIfOnLadder(bool var)
    {   
        SuspendMovement = var;
        _IsOnLadder = var;
        SwitchLadderCam(var);
        Rigidbody.velocity = Vector3.zero;
    }
    public void SetZiplinePoint(Transform Start, Transform End)
    {
        Debug.Log("Set");
        OnZipline = true; 
        StartPos = Start;
        EndPos = End;
    }
    public void ExitZipLine()
    {
        Debug.Log("offladder");
        OnZipline = false;
    }
    public bool GetIfOnLadder()
    {
        return _IsOnLadder;
    }
    public GameObject[] GetCameras()
    {//this is for dodge roll fam
        EnableCams();
        return new GameObject[5] { CameraMain, CameraCrouch, CameraDodge, CameraManager, WeaponCamera };
    }
    //functions for delegates 
    //happy programmer noises ;)
    public void EnableCams()
    {
        CameraCrouch.SetActive(true);
        CameraDodge.SetActive(true);
        CameraMain.SetActive(true);
        WeaponCamera.SetActive(true);
    }
    public void disableCams(bool var)
    {   //set all cameras to false, if called by a function that is setting that camera to false
        //set default cam to main
        CameraCrouch.SetActive(false);
        CameraDodge.SetActive(false);
        CameraMain.SetActive(!var);
    }
  
  
    private void SwitchLadderCam(bool var)
    { 
        disableCams(var);
        CameraDodge.SetActive(var);
    }
 
    public void SetSuspendMovement(bool foo)
    {
        SuspendMovement = foo;
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

    
    private void Move(Vector2 move, float SpeedMod)
    {
        //Perform an initial save when he player moves once
        // unfortunetly can't use Start() functions bc some Start() functions
        // are called before others which produces null references when saving
        if(MovedOnce == false)
        {
            //SavePlugin2.instance.SaveItems();
            MovedOnce = true;
        }

        move = move.normalized;
        if (SuspendMovement == false)
        {
            // was 4.0f
            SpeedMod *= 20.0f;
            mover = transform.right * move.x + transform.forward * move.y;
            Rigidbody.velocity = new Vector3(mover.x * SpeedSlider * SpeedMod, Rigidbody.velocity.y, mover.z * SpeedSlider * SpeedMod);
        }      
       else if (gameObject.GetComponent<Dodge>()!=null  )
       {
            if (GetComponent<Dodge>().GetRollVector() != Vector3.zero) { 
                Rigidbody.velocity = GetComponent<Dodge>().GetRollVector() * 12;
            }
        }
        if (_IsOnLadder == true)
        {

            SpeedMod *= 10.0f;
            // Rigidbody.velocity = Vector3.zero;
            Rigidbody.velocity = new Vector3(0.0f, SpeedMod*move.y/m_LadderSpeed, 0.0f);
        }
        if (OnZipline == true)
        {
            Rigidbody.velocity = SpeedMod*Vector3.Normalize( EndPos.position- StartPos.position);
        }
        Rigidbody.angularVelocity = Vector3.zero;

       
    }
    
  
    // Update is called once per frame
    void Update()
    {
        //dev hack
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Rigidbody.position = new Vector3(170.0f, 25.0f, 420.0f);
        }


            gameObject.transform.rotation = Quaternion.Euler(0.0f, CameraManager.transform.eulerAngles.y, 0.0f);
     //  gameObject.transform.rotation = CameraManager.transform.rotation;


    }


    public bool isGrounded()
    {
        m_Grounded = Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down,2.4f, m_WhatIsGround);
        return Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down, 2.4f, m_WhatIsGround);
    }
 
}
