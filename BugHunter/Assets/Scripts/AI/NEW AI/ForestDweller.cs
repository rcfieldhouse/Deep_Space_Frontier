using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestDweller : AI
{
    [Range(0,50)] public float LaunchHeight;
    [Range(0, 15)] public float TimeBetweenJumps;
   public Rigidbody Rigidbody;
    private bool _CanJump = false,RoarPossible=false,HasFrontAttacked=false;
    public override void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position + transform.rotation * SightRangeOffset, _SightRange, WhatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position + transform.rotation * AttackAreaOffset, _AttackRange, WhatIsPlayer);
        bool playerInAttackRange2 = Physics.CheckSphere(transform.position + transform.rotation * _Attack2AreaOffset, _Attack2_Range, WhatIsPlayer);
      

        if ( _IsHitStunned == false)
        {
            IsSecondaryAttack = playerInAttackRange2;
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) LaunchTheBoss();
            if (playerInAttackRange && playerInSightRange) AttackPlayer(Target);
           
        }
    }
     private void OnDrawGizmos()
    {
        if (Target == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position+PhysicsCalc(Target.transform.position), Vector3.one);
    }
    public override void AttackPlayer(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {
            if (HasFrontAttacked == true)
                IsSecondaryAttack = true;
            if (IsSecondaryAttack == true)
            {
                SecondaryAttack(Target);
                return;
            }
               //play Dante.sound.ogg torterra attack
                AI_Animator.SetBool("_IsAttacking", true);
                Target.GetComponent<HealthSystem>().ModifyHealth(Attack_1_Damage);
                HasAttacked = true;
                CanAttack = false;
            HasFrontAttacked = true;
        }
        if (HasAttacked == true)
        {
            HasAttacked = false;
           Invoke(nameof(ResetAttack), Attack_1_Delay);
            Invoke(nameof(StopAnim), 1.0f);
        }
    }
    void SecondaryAttack(GameObject Target)
    {
        AI_Animator.SetBool("_IsAttacking2", true);
        HasAttacked = true;
        CanAttack = false;
        HasFrontAttacked = false;
        Target.GetComponent<HealthSystem>().ModifyHealth(Attack_2_Damage);
        Invoke(nameof(ResetAttack), Attack_2_Delay);
        Invoke(nameof(StopAnim), 1.0f);
    }
    public override void Patroling()
    {
        RoarPossible = true;
        NavAgent.enabled = false;
       //do nothing bc this man is a boss and he doesn't need to patrol
       //he is the one who knocks
    }
    public void LaunchTheBoss()
    {
        if (RoarPossible == true)
            DoTheRoar();

        if (Target == null)
            Target = FindClosestPlayer();

        Vector3 Dir = Vector3.RotateTowards(transform.forward, Target.transform.position-transform.position, 1*Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(Dir);

       // transform.
        if (_CanJump == true)
        {
            AI_Animator.SetBool("_IsMoving", true);
            Rigidbody.AddForce(Rigidbody.mass*0.8f* PhysicsCalc(Target.transform.position), ForceMode.Impulse);
            _CanJump = false;
            Invoke(nameof(ResetJump),TimeBetweenJumps);
            Invoke(nameof(StopAnim), 1.0f);
        }
    }
    void DoTheRoar()
    {
        RoarPossible = false;   
        AI_Animator.SetBool("_Roar", true);
        Invoke(nameof(ResetJump),4.0f);
        Invoke(nameof(StopAnim), 1.0f);
    }
    public void StopAnim()
    {
        AI_Animator.SetBool("_IsAttacking2", false);
        AI_Animator.SetBool("_IsAttacking", false);
        AI_Animator.SetBool("_Roar", false);
        AI_Animator.SetBool("_IsMoving", false);
    }
    public void ResetJump()
    {  
        _CanJump = true;
    }
    public Vector3 PhysicsCalc(Vector3 Target)
    {
        Vector3 LaunchTarget =(Target-transform.position)/2.5f+Vector3.up*LaunchHeight;
        return LaunchTarget;
    }
}