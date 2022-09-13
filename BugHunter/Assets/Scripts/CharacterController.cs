using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameObject Player;
    [SerializeField]
    private LayerMask m_WhatIsGround;

    [Range(0, 1)][SerializeField] public float m_CrouchSpeed = 0.5f;
    [Range(0, 1)][SerializeField] private float SpeedSlider = .5f;    
    [SerializeField] private Vector3 mover,targetVelocity,JumpForce;
     private bool m_Grounded = true;
    [SerializeField] private CapsuleCollider coll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 move, bool jump,float SpeedMod,Quaternion quaternion)
    {
    
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

        m_Grounded = Physics.BoxCast(coll.bounds.center - Vector3.down / 10, coll.bounds.size / 2, Vector3.down, Quaternion.Euler(Vector3.down), 1f, m_WhatIsGround);
        return Physics.BoxCast(coll.bounds.center - Vector3.down / 10, coll.bounds.size / 2, Vector3.down, Quaternion.Euler(Vector3.down), 1f, m_WhatIsGround);
    }
}
