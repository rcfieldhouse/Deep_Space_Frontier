using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AI
{
  
    // Start is called before the first frame update
    public override void AttackPlayer(GameObject Target)
    {
        GetComponentInChildren<SlimeBounce>().SetIsAttacking(true);
        NavAgent.SetDestination(transform.position);
        if (CanAttack == true && HasAttacked == false)
        {
            if (GetComponentInChildren<SlimeBounce>().GetCanJump() == true)
            {     //play Dante.sound.ogg slime attack
              
               
                AI_Animator.SetBool("_IsAttacking", true);
                Target.GetComponent<HealthSystem>().ModifyHealth(Attack_1_Damage);
                HasAttacked = true;
                CanAttack = false;
            }      
        }
        if (HasAttacked==true)
        {
            HasAttacked=false;
            Invoke(nameof(ResetAttack), Attack_1_Delay);
            Invoke(nameof(ResetAnim), Attack_1_Delay);
        }
      
    }

    // Start is called before the first frame update
    public void ResetAnim()
    {
        AI_Animator.SetBool("_IsAttacking", false);
        GetComponentInChildren<SlimeBounce>().SetIsAttacking(false);
    }

}
