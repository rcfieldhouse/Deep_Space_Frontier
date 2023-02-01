using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Tick : AI
{
    [Range(0,5)] public float DamageTime = 5.0f, DamageInterval = 0.25f;
    [Range(0,-10)]public int Damage = -5;  
    public override void AttackPlayer(GameObject Target)
    {
        transform.LookAt(Target.transform);
        NavAgent.SetDestination(transform.position);
        //play Dante.sound.ogg tick attack
        AI_Animator.SetBool("_IsAttacking", true);
        if (Target.GetComponent<Venom>() == null)
            Target.AddComponent<Venom>().InitAttack(DamageTime,DamageInterval,Damage);

        Invoke(nameof(ResetAnim), 1);
    }
    private void ResetAnim()
    {
        AI_Animator.SetBool("_IsAttacking", false);
    }



}
