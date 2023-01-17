using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AIType
{
   Tick, Beetle, Worm, ElizabethTheDead, Slime, Hound, DreadBomber
}
public class AI : MonoBehaviour
{
    public AIType EnemyType;
    private HealthSystem Health;
    private NavMeshAgent NavAgent;
    private MeshRenderer MeshRenderer;
    private Material[] Materials;
    private GameObject Target;
    //DetectionStuff
    [Range(0, 50)] public float _SightRange = 10, _AttackRange = 2;
    public Vector3 SightRangeOffset, AttackAreaOffset;

    //Attack Stuff
    [Range(0, 50)] public int Attack_1_Damage, Attack_2_Damage;
    [Range(0, 10)] public float Attack_1_Delay, Attack_2_Delay;

    //Death Stuff
    [Range(0.0f, 0.25f)] public float dissolveRate = 0.0125f, refreshRate = 0.02f;
    [Range(0, 8)] public int NumDrops = 0;
    //Navigation Stuff
    public LayerMask WhatIsGround,WhatIsPlayer;
    private Vector3 WalkPoint, SpawnPoint;
    private bool WalkPointSet=false;
    [Range(0, 15)] public float WalkPointRange,WalkSpeed;

    void Awake()
    {
    
            Health = GetComponent<HealthSystem>();
            NavAgent = GetComponent<NavMeshAgent>();
            MeshRenderer = GetComponent<MeshRenderer>();


            switch (EnemyType)
            {
                case AIType.Tick:
                    gameObject.AddComponent<Tick>(); break;
            }


            Debug.Log("AI has been created");

            Health.OnObjectDeath += HandleObjectDeath;

            if (NavAgent.isOnNavMesh == false)
                Debug.Log("NOOOOOO");
            if (MeshRenderer != null)
                Materials = MeshRenderer.materials;
    }
    private void OnDisable()
    {
        Health.OnObjectDeath -= HandleObjectDeath;
        //ScoreManager.instance.sChange(10); 
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + AttackAreaOffset, _AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + SightRangeOffset, _SightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, WalkPointRange);
    }
    public void HandleObjectDeath(GameObject context)
    {
        StartCoroutine(DissolveMeshEffect());
        Debug.Log("dissolving");
    }
    IEnumerator DissolveMeshEffect()
    {
        GetComponent<BoxCollider>().enabled = false;
        float DropRate = 1.0f / (float)NumDrops;
        float DropIndexer = 0.0f;
        NavAgent.enabled = false;
        if (Materials.Length > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < Materials.Length; i++)
                {
                    DropIndexer += dissolveRate;
                    if (DropIndexer > DropRate) { 
                        LootSpawner.instance.SprayLoot(transform);
                        DropIndexer -= DropRate;
                    }
                    Materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }

           

            //this will need to be more elaborate later when we have anims and such, so i'm reworking it now ryan
            //idea is to spray pieces as the object dissolves
            Destroy(gameObject);
        }
    }

    #region Navigation 
    //init stuffs
    public void SetInitialPosition(Vector3 vector3)
    {
        SpawnPoint = vector3;
    }
    public void SetInitialDestination(Vector3 vec)
    {
        WalkPointSet = true;  
        WalkPoint = vec;
    }

    private void Patroling()
    {
        if (!WalkPointSet)
            SearchWalkPoint();

        if (WalkPointSet)
        {
            transform.LookAt(WalkPoint);
            NavAgent.SetDestination(WalkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f)
        {
            WalkPointSet = false;
            SearchWalkPoint();
        }

    }
    private void ChasePlayer()
    {
        if(Target==null)
       Target = FindClosestPlayer();
        NavAgent.SetDestination(Target.transform.position);
    }

    private void AttackPlayer()
    {
        //this will be different per AI thing
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);
        if (Mathf.Abs(randomX) > 1 && Mathf.Abs(randomZ) > 1)
        {
            WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround) == true)
            {
                WalkPointSet = true;
            }
            else SearchWalkPoint();
        }
    }
    #endregion Navigation
    private GameObject FindClosestPlayer()
    {
        GameObject[] AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        float SmallestDistance=100000.0f;
        GameObject foo=null;
        foreach (GameObject Player in AllPlayers)
        {
            if (Mathf.Abs((Player.transform.position - transform.position).magnitude) < SmallestDistance)
            {
                SmallestDistance = Mathf.Abs((Player.transform.position - transform.position).magnitude);
                foo = Player;   
            }             
        }
        return foo;
    }
    // Update is called once per frame
    void Update()
    {
       bool playerInSightRange = Physics.CheckSphere(transform.position, _SightRange, WhatIsPlayer);
       bool playerInAttackRange = Physics.CheckSphere(transform.position, _AttackRange, WhatIsPlayer);
        //these functions can be found in the navigation reigon
        if (NavAgent.enabled == true)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            //if (!playerInAttackRange) SetLunged();

        }
    }
}
