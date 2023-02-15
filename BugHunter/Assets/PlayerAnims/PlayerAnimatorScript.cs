using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerAnimatorScript : MonoBehaviour
{
    public Animator PlayerAnimator;
    private int DirectionMovement,LastDir;
    public Rig LeftArm,RightArm,Neck;
    public PlayerInput Player;
    //Movement Direction
    //0 = N/A 
    //1 = Forwards
    //2 = Backwards
    //3 = Right
    //4 = Left
    public void RestoreWeights()
    {
        LeftArm.weight = 1.0f;
        Neck.weight = 1.0f;
        RightArm.weight = 1.0f;
    }
    private void Awake()
    {
        Player = GetComponent<PlayerInput>();
        Player.SwappingWeapon += SwapWeapon;
        Player.Move += Move;
        Player.WeNeedToCookJesse += CookNade;
        Player.Throw += ThrowGrenade;
        PlayerAnimator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        Player.Move -= Move;
        Player.WeNeedToCookJesse -= CookNade;
        Player.Throw -= ThrowGrenade;
        Player.SwappingWeapon -= SwapWeapon;
    }
    private void SwapWeapon(int num)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/WeaponSwap");
        //cases are 0,1,2 
        //pistols are always on 1, primary is on 0 and relic cannon is on 3
        if (num == 1)
        {
            PlayerAnimator.SetBool("_UsingPistol", true);
            PlayerAnimator.SetBool("_UsingRifle", false);
        }
        else
        {
            PlayerAnimator.SetBool("_UsingPistol", false);
            PlayerAnimator.SetBool("_UsingRifle", true);
        }
    }

    public void Dodge(bool var)
    {      
        if (var == true)
        {
            LeftArm.weight = 0.0f;
            Neck.weight = 0.0f;
            RightArm.weight = 0.0f;
        }
        else RestoreWeights();

        PlayerAnimator.SetBool("_IsDodging", var);
    }
    private void CookNade()
    {
        LeftArm.weight = 0;
        Neck.weight = 0;
        RightArm.weight = 0;
        PlayerAnimator.SetBool("_CookGrenade", true);
        PlayerAnimator.SetBool("_ThrowGrenade", false);
    }
    private void ThrowGrenade(Quaternion quaternion)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Projectiles/Grenade_Throw");
        LeftArm.weight = 0;
        Neck.weight = 0;
        RightArm.weight = 0;
        PlayerAnimator.SetBool("_ThrowGrenade", true);
        PlayerAnimator.SetBool("_CookGrenade", false);
        Invoke(nameof(RestoreWeights), 1.0f);
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

        if (LastDir != DirectionMovement||Dir==Vector2.zero) PlayerAnimator.SetBool("_ChangeWalk", true);
        else PlayerAnimator.SetBool("_ChangeWalk", false);

        PlayerAnimator.SetFloat("MovementDirection", DirectionMovement);
        LastDir = DirectionMovement;
        //Debug.Log(DirectionMovement);
    }
    public void Jump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Player_Jump");
        PlayerAnimator.SetBool("_StartJump", true);
    }
    public void Land(bool var)
    {
        PlayerAnimator.SetBool("_EndJump", var);

        if(var==true) PlayerAnimator.SetBool("_StartJump", false);
    }

}
