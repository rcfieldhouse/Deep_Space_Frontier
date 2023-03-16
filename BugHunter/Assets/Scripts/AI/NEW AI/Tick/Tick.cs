using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Tick : AI
{

    [Range(0,5)] public float VenomDamageTime = 5.0f, VenomDamageInterval = 0.25f;
    [Range(0,-10)]public int VenomDamage = -5;
    public override void AttackPlayer(GameObject Target)
    {
    if (CanAttack == true && HasAttacked == false)
    {
        transform.LookAt(Target.transform);
        NavAgent.SetDestination(transform.position);
        //play Dante.sound.ogg tick attack
        AI_Animator.SetBool("_IsAttacking", true);
        Target.GetComponent<HealthSystem>().ModifyHealth(gameObject, Attack_1_Damage);
        HasAttacked = true;
        CanAttack = false;

        if (Target.GetComponent<Venom>() == null)
            Target.AddComponent<Venom>().InitAttack(VenomDamageTime, VenomDamageInterval, VenomDamage);
    }
        if (HasAttacked == true)
        {
            HasAttacked = false;
            Invoke(nameof(ResetAttack), Attack_1_Delay);
            Invoke(nameof(ResetAnim), Attack_1_Delay);
        }
    }
    private void ResetAnim()
    {
        AI_Animator.SetBool("_IsAttacking", false);
    }



}
