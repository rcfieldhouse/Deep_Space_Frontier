using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : AI
{
    // Start is called before the first frame update
   
    public List<Transform> ProjectileSpawns;
    [Range(0, 100)]
    public float SpawnEnemiesRange;
    public GameObject Projectile,Tick,Zephyr;
    [Range(0, 50)] public float ProjectileSpeed = 10;
    // Update is called once per frame
    public override void Patroling()
    {
        base.Patroling();
        AI_Animator.SetBool("ForwardMove", true);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SpawnEnemiesRange);
    }
    public override void AttackPlayer(GameObject Target)
    {
        //Debug.Log(IsSecondaryAttack);
        if (IsSecondaryAttack == false)
        {
            //allignment and avoidance
            AI_Animator.SetBool("BackwardMove", true);
            AI_Animator.SetBool("ForwardMove", false);
            NavAgent.destination = transform.position;
            NavAgent.SetDestination(transform.position + (-WalkPoint) + transform.rotation * new Vector3(Serpentine, 0.0f, 0.0f));
            //primary shooting attack
            if (CanAttack == true && HasAttacked == false)
            {        
                for (int i = 0; i < ProjectileSpawns.Count*3; i++)
                {
                    Debug.Log(i);
                    Rigidbody rb = Instantiate(Projectile, ProjectileSpawns[i%4].position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(Vector3.Normalize(Target.transform.position - (ProjectileSpawns[i%4].position)) * (ProjectileSpeed+(i*2)), ForceMode.Impulse);
                    rb.gameObject.GetComponent<Thorn>().SetDamage(Attack_1_Damage);
                    rb.gameObject.transform.LookAt(Target.transform);
                }
             
                    HasAttacked = true;
                CanAttack = false;
            }
            if (HasAttacked == true)
            {
               //AI_Animator.SetBool("_IsAttacking", false);
                HasAttacked = false;
                Invoke(nameof(ResetAttack), Attack_1_Delay);
            }
           
        }
        else if (IsSecondaryAttack == true)
        {
            AI_Animator.SetBool("BackwardMove", false);
            AI_Animator.SetBool("ForwardMove", true);
            NavAgent.SetDestination(transform.position + WalkPoint + transform.rotation * new Vector3(Serpentine, 0.0f, 0.0f));
        }

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, Target.transform.position-transform.position, NavAgent.angularSpeed*Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
      
    }
    public override void ChasePlayer()
    {
        _IsChasing = true;
        if (Target == null)
            Target = FindClosestPlayer();


        if (WalkPointSet == false)
        {

            Serpentine = Random.Range(-EvasionIntensity, EvasionIntensity);
            StartEvasionLocation = transform.position;
            WalkPointSet = true;
            NavAgent.SetDestination(transform.position + WalkPoint + transform.rotation * new Vector3(Serpentine, 0.0f, 0.0f));
        }

        DistanceTravelled = transform.position - StartEvasionLocation;
        AI_Animator.SetBool("ForwardMove", true);
        AI_Animator.SetBool("BackwardMove", false);
        if (DistanceTravelled.magnitude > EvasionRecalculationPeriod)
        {
            WalkPointSet = false;
            WalkPoint = Vector3.Normalize(Target.transform.position - transform.position);
            WalkPoint *= WalkPointRange;
        }
        NavAgent.SetDestination(transform.position + WalkPoint + transform.rotation * new Vector3(Serpentine, 0.0f, 0.0f));


        if (NavAgent.remainingDistance < 0.25f)
            WalkPointSet = false;


        bool attackDirectly = Physics.CheckSphere(transform.position, WalkPointRange, WhatIsPlayer);

        if (attackDirectly == true)
            NavAgent.SetDestination(Target.transform.position);

    }

}
