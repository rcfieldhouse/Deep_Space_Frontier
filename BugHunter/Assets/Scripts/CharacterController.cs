using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameObject Player;
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;
    [Range(0, 1)][SerializeField] private float SpeedSlider = .5f;    
    [SerializeField] private Vector3 mover,targetVelocity,JumpForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 move, bool jump,float Sprint,Quaternion quaternion)
    {
        rigidbody.gameObject.transform.localRotation = quaternion;
         mover = transform.right * move.x + transform.forward * move.y;
      
             targetVelocity = new Vector3(mover.x * SpeedSlider * 5 * Sprint, rigidbody.velocity.y, mover.z * SpeedSlider * 5 * Sprint);

      if (jump == true)
        {
            targetVelocity += JumpForce;

        }
       

        // And then smoothing it out and applying it to the character
        rigidbody.velocity = targetVelocity;
        rigidbody.angularVelocity = Vector3.zero;
     
    }
}
