using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : AI
{
    public GameObject Projectile;
    [Range(0, 20)] public float ProjectileSpeed = 10;
    FMOD.Studio.EventInstance rangedSound;
    FMOD.Studio.EventInstance meleeSound;
    public override void Update()
    {
        Target = FindClosestPlayer();
        base.Update();
    }
    public override void AttackPlayer(GameObject Target)
    {
       
      //  Debug.Log(Mathf.Abs((transform.position - Target.transform.position).magnitude));
        if (IsSecondaryAttack == true)
        {
            if (Mathf.Abs((transform.position - Target.transform.position).magnitude) < 5.0f)
            {
               SecondaryAttack(Target);
            }
            if (HasAttacked == false && CanAttack == true)
            {
                AI_Animator.SetBool("_IsMoving", true);
                NavAgent.speed = WalkSpeed*1.5f;
                NavAgent.SetDestination(Target.transform.position);
            }
              
        }
        else
        {
            if (CanAttack == true && HasAttacked == false)
            {
               
                AI_Animator.SetBool("_IsAttacking", true);
                //play Dante.sound.ogg Zephyr ranged attack

                rangedSound = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Zephyr");
                rangedSound.start();
                rangedSound.release();

                transform.LookAt(Target.transform);
                Transform TheBug = GetComponentInChildren<SkinnedMeshRenderer>().gameObject.transform;
                Rigidbody rb = Instantiate(Projectile, TheBug.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(Vector3.Normalize(Target.transform.position - (TheBug.position)) * ProjectileSpeed, ForceMode.Impulse);
                rb.gameObject.GetComponent<Thorn>().SetDamage(Attack_1_Damage);
                rb.gameObject.transform.LookAt(Target.transform);

                HasAttacked = true;
                CanAttack = false;
            }
            if (HasAttacked == true)
            {
                AI_Animator.SetBool("_IsAttacking", false);
                HasAttacked = false;
                Invoke(nameof(ResetAttack), Attack_1_Delay);
            }
        }
     
    }
    public void SecondaryAttack(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {

            //play Dante.sound.ogg zephyr melee attack

            meleeSound = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Zephyr");
            meleeSound.start();
            meleeSound.release();

            NavAgent.SetDestination(transform.position+((transform.position- Target.transform.position).normalized * WalkPointRange));
            Target.GetComponent<HealthSystem>().ModifyHealth(gameObject, Attack_2_Damage);
            HasAttacked = true;
            CanAttack = false;
        }
        if (HasAttacked == true)
        {
            AI_Animator.SetBool("_IsMoving", false);
            NavAgent.speed = WalkSpeed;
            HasAttacked = false;
            Invoke(nameof(ResetAttack), Attack_2_Delay);
        
        }
    }

    // Start is called before the first frame update



}
