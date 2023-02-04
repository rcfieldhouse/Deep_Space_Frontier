using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    public Animator PlayerAnimator;
    private int DirectionMovement,LastDir;
    
    //Movement Direction
    //0 = N/A 
    //1 = Forwards
    //2 = Backwards
    //3 = Right
    //4 = Left

    private void Awake()
    {
        PlayerInput.Move += Move;
        PlayerInput.WeNeedToCookJesse += CookNade;
        PlayerInput.Throw += ThrowGrenade;
        PlayerAnimator = GetComponent<Animator>();
    }
    public void Dodge(bool var)
    {
        PlayerAnimator.SetBool("_IsDodging", var);
    }
    private void CookNade()
    {
        PlayerAnimator.SetBool("_CookGrenade", true);
    }
    private void ThrowGrenade(Quaternion quaternion)
    {
        PlayerAnimator.SetBool("_ThrowGrenade", true);
    }
    private void Move(Vector2 Dir, float Running)
    {
        if (Running == 1.5f) PlayerAnimator.SetBool("_IsRunning", true);
        else PlayerAnimator.SetBool("_IsRunning", false);

        if (Dir == Vector2.zero) DirectionMovement = 0;    
        if (Dir == Vector2.right) DirectionMovement = 3;
        if (Dir == Vector2.left) DirectionMovement = 4;
        if (Dir == Vector2.up) DirectionMovement = 1;
        if (Dir == Vector2.down) DirectionMovement = 2;

        if (LastDir != DirectionMovement) PlayerAnimator.SetBool("_ChangeWalk", true);
        else PlayerAnimator.SetBool("_ChangeWalk", false);

        PlayerAnimator.SetFloat("MovementDirection", DirectionMovement);
        LastDir = DirectionMovement;
        //Debug.Log(DirectionMovement);
    }
    public void Jump()
    {
        PlayerAnimator.SetBool("_StartJump", true);
    }
    public void Land(bool var)
    {
        PlayerAnimator.SetBool("_EndJump", var);

        if(var==true) PlayerAnimator.SetBool("_StartJump", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
