using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBounce : MonoBehaviour
{
    [Range(0, 25)] public float JumpHeight=15;
    [Range(0, 3)] public float HeightSlime = 2;
    bool Dead = false;
    private bool Var = false,CanJump=true,_IsAttacking=false;
    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(GetComponent<CapsuleCollider>().bounds.center, GetComponent<CapsuleCollider>().bounds.center - new Vector3(0.0f, HeightSlime, 0.0f));
    }
    // Update is called once per frame
    void Update()
    {
        CanJump = Physics.Raycast(GetComponent<CapsuleCollider>().bounds.center, Vector3.down, HeightSlime, GetComponentInParent<Slime>().WhatIsGround);
        if (Physics.Raycast(GetComponent<CapsuleCollider>().bounds.center, Vector3.down, HeightSlime, GetComponentInParent<Slime>().WhatIsGround) == true)
            Invoke(nameof(Jump),0.1f);

        if (GetComponent<HealthSystem>().GetHealth() <= 0)
            Dead = true;

        if (Dead == true)
            transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        
        }
    private void Jump()
    {
        if (_IsAttacking == false&&GetComponentInParent<Slime>()._IsHitStunned==false)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * JumpHeight;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Creature/Slime_Jump");
            Var = !Var;
            GetComponentInParent<AI>().AI_Animator.SetBool("_IsMoving", Var);
            if (Dead == true)
            {
                transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }    
    }
    public void SetIsAttacking(bool var)
    {
        _IsAttacking = var;
    }
    public bool GetCanJump()
    {
        return CanJump;
    }
}
