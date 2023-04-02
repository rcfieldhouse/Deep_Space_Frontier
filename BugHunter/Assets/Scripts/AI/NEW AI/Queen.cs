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
    [Range(0, 30)] public float CarpetBombSpread,AOEBombSpread;
    public List<Vector3> BombingLocations,AOEBombingLocations;
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
        Vector3 vec6 = new Vector3(CarpetBombSpread*0.75f, 0.0f, CarpetBombSpread * 0.75f);
        Vector3 vec7 = new Vector3(CarpetBombSpread * 0.75f, 0.0f, -CarpetBombSpread * 0.75f);
        Vector3 vec8 = new Vector3(-CarpetBombSpread * 0.75f, 0.0f, CarpetBombSpread * 0.75f);
        Vector3 vec9 = new Vector3(-CarpetBombSpread * 0.75f, 0.0f,-CarpetBombSpread * 0.75f);
        BombingLocations.Add(vec1);
        BombingLocations.Add(vec2);
        BombingLocations.Add(vec3);
        BombingLocations.Add(vec4);
        BombingLocations.Add(vec5);
        BombingLocations.Add(vec6);
        BombingLocations.Add(vec7);
        BombingLocations.Add(vec8);
        BombingLocations.Add(vec9);

        AOEBombSpread*=5;
        Vector3 vector2 = new Vector3(AOEBombSpread, 0.0f, 0.0f);
        Vector3 vector3 = new Vector3(-AOEBombSpread, 0.0f, 0.0f);
        Vector3 vector4 = new Vector3(0.0f, 0.0f, AOEBombSpread);
        Vector3 vector5 = new Vector3(0.0f, 0.0f, -AOEBombSpread);
        Vector3 vector6 = new Vector3(AOEBombSpread / 2, 0.0f, AOEBombSpread / 2);
        Vector3 vector7 = new Vector3(AOEBombSpread / 2, 0.0f, -AOEBombSpread / 2);
        Vector3 vector8 = new Vector3(-AOEBombSpread / 2, 0.0f, AOEBombSpread / 2);
        Vector3 vector9 = new Vector3(-AOEBombSpread / 2, 0.0f, -AOEBombSpread / 2);

        Vector3 vector10 = new Vector3(AOEBombSpread*1.25f, 0.0f, AOEBombSpread/2);
        Vector3 vector11 = new Vector3(-AOEBombSpread * 1.25f, 0.0f, AOEBombSpread / 2);
        Vector3 vector12 = new Vector3(-AOEBombSpread / 2, 0.0f, AOEBombSpread * 1.25f);
        Vector3 vector13 = new Vector3(-AOEBombSpread / 2, 0.0f, -AOEBombSpread * 1.25f);

        Vector3 vector14 = new Vector3(AOEBombSpread*1.25f, 0.0f, -AOEBombSpread / 2);
        Vector3 vector15 = new Vector3(-AOEBombSpread * 1.25f, 0.0f, -AOEBombSpread / 2);
        Vector3 vector16 = new Vector3(AOEBombSpread / 2, 0.0f, AOEBombSpread * 1.25f);
        Vector3 vector17 = new Vector3(AOEBombSpread / 2, 0.0f, -AOEBombSpread * 1.25f);

       AOEBombingLocations.Add(vector2);
       AOEBombingLocations.Add(vector3);
       AOEBombingLocations.Add(vector4);
       AOEBombingLocations.Add(vector5);
       AOEBombingLocations.Add(vector6);
       AOEBombingLocations.Add(vector7);
       AOEBombingLocations.Add(vector8);
       AOEBombingLocations.Add(vector9);
       AOEBombingLocations.Add(vector10);
       AOEBombingLocations.Add(vector11);
       AOEBombingLocations.Add(vector12);
       AOEBombingLocations.Add(vector13);
       AOEBombingLocations.Add(vector14);
       AOEBombingLocations.Add(vector15);
       AOEBombingLocations.Add(vector16);
       AOEBombingLocations.Add(vector17);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SpawnEnemiesRange);


        if (Target == null)
            return;
        Gizmos.color = Color.magenta;
    

        Vector3 P1 = VollyLaunchPoint.position + Vector3.up * 10;
        Vector3 P2=Vector3.zero;
      

      for (int i = 0; i < AOEBombingLocations.Count ; i++)
      {
          Gizmos.DrawSphere(VollyLaunchPoint.position + transform.rotation* AOEBombingLocations[i], 1);
      }
      for (int j = 0; j < AOEBombingLocations.Count; j++)
      {
          P2 =( transform.rotation * AOEBombingLocations[j]) + VollyLaunchPoint.position;
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

        Gizmos.DrawCube(VollyLaunchPoint.position + Vector3.up * 10, Vector3.one);
        Gizmos.DrawCube(Target.transform.position, Vector3.one);
        // Gizmos.DrawCube(P1+ PhysicsCalc(), Vector3.one);
        // Gizmos.DrawLine(P1, P1 + PhysicsCalc());
        for (int j = 0; j < 9 ; j++)
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

    void CarpetBombAttack(Vector3 P1)
    {
        for (int j = 0; j < 9; j++)
        {
            Vector3 P2 = BombingLocations[j] + Player.transform.position;
            GameObject obj = Instantiate(AOE_Orb, VollyLaunchPoint.position + Vector3.up * 10, Quaternion.identity);
            obj.gameObject.AddComponent<CurveyTrajectory>().SetValues(P1, P2, LaunchHeight, j);
        }
    }
    void AOEBombAttack(Vector3 P1)
    {
        for (int j = 0; j < AOEBombingLocations.Count; j++)
        {
           Vector3 P2 = VollyLaunchPoint.position+ transform.rotation* AOEBombingLocations[j];
            GameObject obj = Instantiate(AOE_Orb, VollyLaunchPoint.position + Vector3.up * 10, Quaternion.identity);
            obj.gameObject.AddComponent<CurveyTrajectory>().SetValues(P1, P2, LaunchHeight, j);
        }
    }
    void SpawnCreatures()
    {
        for (int i = 0; i < EnemySpawns.Count; i++)
        {
            if (i != 2 && i != 3)
                Instantiate(Tick, EnemySpawns[i].position, Quaternion.identity);
            else
                Instantiate(Zephyr, EnemySpawns[i].position, Quaternion.identity);
        }
    }
    void ThornVolley()
    {
        for (int i = 0; i < ProjectileSpawns.Count * 3; i++)
        {
            // Debug.Log(i);
            Rigidbody rb = Instantiate(Projectile, ProjectileSpawns[i % 4].position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(Vector3.Normalize(Target.transform.position - (ProjectileSpawns[i % 4].position)) * (ProjectileSpeed + (i * 2)), ForceMode.Impulse);
            rb.gameObject.GetComponent<Thorn>().SetDamage(Attack_1_Damage);
            rb.gameObject.transform.LookAt(Target.transform);
        }
    }
    public override void AttackPlayer(GameObject Target)
    {
       // Debug.Log((Target.transform.position - transform.position).magnitude);
       Debug.Log((Target.transform.position - transform.position).magnitude );
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

                if ((Target.transform.position - transform.position).magnitude <= 50)
                    AOEBombAttack(P1);
                if ((Target.transform.position - transform.position).magnitude > 50 && (Target.transform.position - transform.position).magnitude < 70)
                    ThornVolley();
                if ((Target.transform.position - transform.position).magnitude >= 70)
                    CarpetBombAttack(P1);

                AI_Animator.SetBool("ShootingProjectile", true);
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
                SpawnCreatures();
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
