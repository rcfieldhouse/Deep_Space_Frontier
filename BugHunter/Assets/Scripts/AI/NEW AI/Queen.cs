using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : AI
{
    // Start is called before the first frame update
   
    public List<Transform> ProjectileSpawns,EnemySpawns;
    [Range(0, 100)]
    public float SpawnEnemiesRange;
    public GameObject Projectile,Tick,Zephyr,AOE_Orb;
    [Range(0, 50)] public float ProjectileSpeed = 10;
    private GameObject Player;
    public Transform VollyLaunchPoint;
    [Range(0, 50)] public float LaunchHeight;
    [Range(0, 15)] public float CarpetBombSpread;
    public List<Vector3> BombingLocations;
    // Update is called once per frame
    public override void Patroling()
    {
        base.Patroling();
        AI_Animator.SetBool("ForwardMove", true);
    }
    public override void Awake()
    {
        base.Awake();
        Vector3 vec1 = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 vec2 = new Vector3(CarpetBombSpread, 0.0f, 0.0f);
        Vector3 vec3 = new Vector3(-CarpetBombSpread, 0.0f, 0.0f);
        Vector3 vec4 = new Vector3(0.0f, 0.0f, CarpetBombSpread);
        Vector3 vec5 = new Vector3(0.0f, 0.0f,-CarpetBombSpread);
        Vector3 vec6 = new Vector3(CarpetBombSpread, 0.0f, CarpetBombSpread);
        Vector3 vec7 = new Vector3(CarpetBombSpread, 0.0f, -CarpetBombSpread);
        Vector3 vec8 = new Vector3(-CarpetBombSpread, 0.0f, CarpetBombSpread);
        Vector3 vec9 = new Vector3(-CarpetBombSpread, 0.0f,-CarpetBombSpread);
        BombingLocations.Add(vec1);
        BombingLocations.Add(vec2);
        BombingLocations.Add(vec3);
        BombingLocations.Add(vec4);
        BombingLocations.Add(vec5);
        BombingLocations.Add(vec6);
        BombingLocations.Add(vec7);
        BombingLocations.Add(vec8);
        BombingLocations.Add(vec9);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SpawnEnemiesRange);


        if (Target == null)
            return;
        Gizmos.color = Color.magenta;
      
        Gizmos.DrawCube(VollyLaunchPoint.position + Vector3.up * 10, Vector3.one);
        Gizmos.DrawCube(Target.transform.position, Vector3.one);
        Vector3 P1 = VollyLaunchPoint.position + Vector3.up * 10;
        Vector3 P2 = Player.transform.position;
        Gizmos.DrawCube(SampleParabola(P1, P2, LaunchHeight, 0.5f),Vector3.one);


        // Gizmos.DrawCube(P1+ PhysicsCalc(), Vector3.one);
        // Gizmos.DrawLine(P1, P1 + PhysicsCalc());
        for (int j = 0; j < 9 + 1; j++)
        {
             P2 = BombingLocations[j]+ Player.transform.position;
            float count = 20;
            Vector3 lastP = P1;
            for (float i = 0; i < count + 1; i++)
            {
                Vector3 p = SampleParabola(P1, P2, LaunchHeight, i / count);
                Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
                Gizmos.DrawLine(lastP, p);
                lastP = p;
            }
        }

    
       // LaunchForce = Distance / 2.5f;
    }
    Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
            return result;
        }
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
                Vector3 P1 = VollyLaunchPoint.position + Vector3.up * 10;
                Vector3 P2 = Player.transform.position;
              
                for (int j = 0; j < 9; j++)
                {
                    P2 = BombingLocations[j] + Player.transform.position;       
                    GameObject obj = Instantiate(AOE_Orb, VollyLaunchPoint.position + Vector3.up * 10, Quaternion.identity);
                    obj.gameObject.AddComponent<CurveyTrajectory>().SetValues(P1, P2, LaunchHeight);
                }
               
                
                AI_Animator.SetBool("ShootingProjectile", true);
                for (int i = 0; i < ProjectileSpawns.Count*3; i++)
                {
                   // Debug.Log(i);
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
                Invoke(nameof(ResetAnims),0.25f);
                HasAttacked = false;
                Invoke(nameof(ResetAttack), Attack_1_Delay);
            }
           
        }
        else if (IsSecondaryAttack == true&& CanAttack == true && HasAttacked == false)
        {
            AI_Animator.SetBool("BackwardMove", false);
            AI_Animator.SetBool("ForwardMove", true);
            NavAgent.SetDestination(transform.position + WalkPoint + transform.rotation * new Vector3(Serpentine, 0.0f, 0.0f));
            bool AbleToSpawn = Physics.CheckSphere(transform.position, SpawnEnemiesRange, WhatIsPlayer);
            if (AbleToSpawn)
            {
                for (int i = 0; i < EnemySpawns.Count; i++)
                {
                    if(i!= 2 && i != 3)
                     Instantiate(Tick, EnemySpawns[i].position, Quaternion.identity);
                    else
                     Instantiate(Zephyr, EnemySpawns[i].position, Quaternion.identity);
                }
                HasAttacked = true;
                CanAttack = false;               
             }
            if (HasAttacked == true)
            {
                AI_Animator.SetBool("Shake", true);
                Invoke(nameof(ResetAnims), 0.25f);
                HasAttacked = false;
                Invoke(nameof(ResetAttack), Attack_2_Delay);

            }
        }

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, Target.transform.position-transform.position, NavAgent.angularSpeed*Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
      
    }
    public void ResetAnims()
    {
        AI_Animator.SetBool("Shake", false);
        AI_Animator.SetBool("ShootingProjectile", false);
    }
    public override void ChasePlayer()
    {
        _IsChasing = true;
        if (Target == null)
            Target = FindClosestPlayer();
        Player = Target;

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
