using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AI
{
    // Start is called before the first frame update
    public override void AttackPlayer(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {
            Target.GetComponent<HealthSystem>().ModifyHealth(Attack_1_Damage);
            HasAttacked = true;
            CanAttack = false;
        }
        if (HasAttacked==true)
        {
            HasAttacked=false;
            Invoke(nameof(ResetAttack), Attack_1_Delay);
        }
      
    }

    // Start is called before the first frame update


}
