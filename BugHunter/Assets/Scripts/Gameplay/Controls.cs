using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameObject Player;
    public GameObject Cam,CrouchCam;
 
    [SerializeField] private LayerMask m_WhatIsGround;

    [Range(0, 1)][SerializeField] public float m_CrouchSpeed = 0.5f;
    [Range(0, 1)][SerializeField] private float SpeedSlider = .5f;    
    [SerializeField] private Vector3 mover,targetVelocity,JumpForce;
     private bool m_Grounded = true;
    [SerializeField] private CapsuleCollider coll;

    public void Move(Vector2 move, bool jump,float SpeedMod,Quaternion quaternion, bool crouch)
    {
        Cam.SetActive(false);
        CrouchCam.SetActive(false);

        if (crouch == true)
        {       
            CrouchCam.SetActive(true);
            Cam.SetActive(false);
        }
    else if (crouch != true)
        {
            CrouchCam.SetActive(false);
            Cam.SetActive(true);

        }

        //lock camera if looking above where we want so that they cant rotate indefinatley on the up down axis 
        //Debug.Log(Player.transform.localRotation.x);
        //if (Player.transform.rotation.x < -90.0f )
        //{
        //    //quaternion.x -= (quaternion.x - 0.65f);
        //    
        //    Debug.Log("ahhhhhh");         
        //}

        rigidbody.gameObject.transform.localRotation = quaternion;
        mover = transform.right * move.x + transform.forward * move.y;
      
             targetVelocity = new Vector3(mover.x * SpeedSlider * 5 * SpeedMod, rigidbody.velocity.y, mover.z * SpeedSlider * 5 * SpeedMod);

      if (jump == true)
        {
            targetVelocity += JumpForce;

        }
       

        // And then smoothing it out and applying it to the character
        rigidbody.velocity = targetVelocity;
        rigidbody.angularVelocity = Vector3.zero;
     
    }
    public bool isGrounded()
    {
       //Debug.Log(Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down,1.2f, m_WhatIsGround));
        m_Grounded = Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down, 1.2f, m_WhatIsGround);
        return Physics.Raycast(coll.bounds.center - Vector3.down / 10, Vector3.down, 1.2f, m_WhatIsGround);
    }
}
