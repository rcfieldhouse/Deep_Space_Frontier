using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : AI
{
    public GameObject Projectile;
    [Range(0, 20)] public float ProjectileSpeed = 10; 
    public override void AttackPlayer(GameObject Target)
    {
        if (IsSecondaryAttack == true)
        {
            if (Mathf.Abs((transform.position - Target.transform.position).magnitude) < 4.0f)
            {
               SecondaryAttack(Target);
            }
            if (HasAttacked == false && CanAttack == true)
            {
                NavAgent.speed = WalkSpeed*1.5f;
                NavAgent.SetDestination(Target.transform.position);
            }
              
        }
        else
        {
            if (CanAttack == true && HasAttacked == false)
            {
                transform.LookAt(Target.transform);
                Transform TheBug = GetComponentInChildren<MeshRenderer>().gameObject.transform;
                Rigidbody rb = Instantiate(Projectile, TheBug.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(Vector3.Normalize(Target.transform.position - (TheBug.position)) * ProjectileSpeed, ForceMode.Impulse);
                rb.gameObject.GetComponent<Thorn>().SetDamage(Attack_1_Damage);
                rb.gameObject.transform.LookAt(Target.transform);

                HasAttacked = true;
                CanAttack = false;
            }
            if (HasAttacked == true)
            {
                HasAttacked = false;
                Invoke(nameof(ResetAttack), Attack_1_Delay);
            }
        }
     
    }
    public void SecondaryAttack(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {
            NavAgent.SetDestination(transform.position+((transform.position- Target.transform.position).normalized * WalkPointRange));
            Target.GetComponent<HealthSystem>().ModifyHealth(Attack_2_Damage);
            HasAttacked = true;
            CanAttack = false;
        }
        if (HasAttacked == true)
        {
            NavAgent.speed = WalkSpeed;
            HasAttacked = false;
            Invoke(nameof(ResetAttack), Attack_2_Delay);
        }
    }
    // Start is called before the first frame update

  

}
