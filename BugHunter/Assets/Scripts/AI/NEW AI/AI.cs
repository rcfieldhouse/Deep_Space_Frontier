using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{   
    [HideInInspector] public HealthSystem Health;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public MeshRenderer MeshRenderer;
    [HideInInspector] public Material[] Materials;
    [HideInInspector] public GameObject Target;

    //DetectionStuff
    [Range(0, 50)] public float _SightRange = 10, _AttackRange = 2;
    public Vector3 SightRangeOffset, AttackAreaOffset;

    //Attack Stuff
    [Range(0, -50)] public int Attack_1_Damage, Attack_2_Damage;
    [Range(0, 10)] public float Attack_1_Delay, Attack_2_Delay;
    [HideInInspector] public bool CanAttack=true,HasAttacked=false;
    //Death Stuff
    [Range(0.0f, 0.25f)] public float dissolveRate = 0.0125f, refreshRate = 0.02f;
    [Range(0, 8)] public int NumDrops = 0;

    //Navigation Stuff
    //Dante, Before you ask, Serpentine is the thing i use to make the enemy go side to side
    private Vector3 StartEvasionLocation,DistanceTravelled = Vector3.zero;
    private float Serpentine = 0.0f;
    [Range(0, 10)] public float EvasionIntensity=0, EvasionRecalculationTime=0;
    public LayerMask WhatIsGround,WhatIsPlayer;
    private Vector3 WalkPoint, SpawnPoint,Pos;
    private bool WalkPointSet=false;
    [Range(0, 15)] public float WalkPointRange,WalkSpeed;

    public void Awake()
    {
        Health = GetComponentInChildren<HealthSystem>();
        NavAgent = GetComponent<NavMeshAgent>();
        MeshRenderer = GetComponentInChildren<MeshRenderer>();

        Health.OnObjectDeath += HandleObjectDeath;
        Health.OnHealthPercentChanged += HandleObjectHit;
        if (NavAgent.isOnNavMesh == false)
            Debug.Log("NOOOOOO");
        if (MeshRenderer != null)
            Materials = MeshRenderer.materials;

        NavAgent.speed = WalkSpeed;
        
        //essentially makes them not dumb
        StartCoroutine(PatrolCorrection());
    }
    public void Update()
    {
       
        bool playerInSightRange = Physics.CheckSphere(transform.position+ transform.rotation* SightRangeOffset, _SightRange, WhatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere( transform.position + transform.rotation * AttackAreaOffset, _AttackRange, WhatIsPlayer);
        //these functions can be found in the navigation reigon
        if (NavAgent.enabled == true)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer(Target);
            //if (!playerInAttackRange) SetLunged();
        }
    }
    public void OnDisable()
    {
        Health.OnObjectDeath -= HandleObjectDeath;
        Health.OnHealthPercentChanged -= HandleObjectHit;
        //ScoreManager.instance.sChange(10); 
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position +transform.rotation*AttackAreaOffset, _AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * SightRangeOffset, _SightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, WalkPointRange);
    }
    #region TakeDamage
    public void HandleObjectHit(float Hit)
    {
        for (int i = 0; i < transform.childCount; i++)
        if(transform.GetChild(i).gameObject.GetComponent<VFX_ID>()!=null)
                transform.GetChild(i).gameObject.GetComponent<VFX_ID>().gameObject.SetActive(true);
    }
    public void HandleObjectDeath(GameObject context)
    {
        StartCoroutine(DissolveMeshEffect());
        Debug.Log("dissolving");
    }
    IEnumerator DissolveMeshEffect()
    {
        //GetComponentInChildren<BoxCollider>().enabled = false;
      
        float DropRate = 1.0f / (float)NumDrops;
        float DropIndexer = 0.0f;
        NavAgent.enabled = false;
        if (Materials.Length > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                DropIndexer += dissolveRate;
                if (DropIndexer > DropRate)
                {
                    LootSpawner.instance.SprayLoot(transform);
                    DropIndexer -= DropRate;
                }
                for (int i = 0; i < Materials.Length; i++)
                {           
                    Materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }

           

            //this will need to be more elaborate later when we have anims and such, so i'm reworking it now ryan
            //idea is to spray pieces as the object dissolves
            Destroy(gameObject);
        }
    }

    #endregion TakeDamage

    #region Attack
    public void ResetAttack()
    {
        CanAttack = true;
    }
    private GameObject FindClosestPlayer()
    {
        GameObject[] AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        float SmallestDistance = 100000.0f;
        GameObject foo = null;
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

    public abstract void AttackPlayer(GameObject Target);
    #endregion Attack

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

    public void Patroling()
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
    public void ChasePlayer()
    {
        if(Target==null)
       Target = FindClosestPlayer();


        if (WalkPointSet == false)
        {
            Serpentine = Random.Range(-EvasionIntensity, EvasionIntensity);
            StartEvasionLocation = transform.position;
            WalkPointSet = true;
        }
        DistanceTravelled = transform.position - StartEvasionLocation;

         if (DistanceTravelled.magnitude > EvasionRecalculationTime)
             WalkPointSet = false;

        WalkPoint = Vector3.Normalize(Target.transform.position - transform.position);
        WalkPoint *= WalkPointRange;
        Debug.Log(DistanceTravelled.magnitude);
        
        NavAgent.SetDestination(transform.position+ WalkPoint+ transform.rotation * new Vector3(Serpentine, 0.0f,0.0f));
    }


    public void SearchWalkPoint()
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
    private IEnumerator PatrolCorrection()
    {
        if (NavAgent.isOnNavMesh)
        {
             Pos = gameObject.transform.position;
        }
        yield return new WaitForSeconds(2.0f);
        if (NavAgent.isOnNavMesh)
        {
            if (Mathf.Abs(Pos.x - gameObject.transform.position.x) < 0.1f)
            {
                WalkPointSet = false;
            }
            if ((Mathf.Abs(gameObject.transform.position.x - SpawnPoint.x) > 60.0f) || (Mathf.Abs(gameObject.transform.position.y - SpawnPoint.y) > 60.0f))
            {
                NavAgent.SetDestination(SpawnPoint);
                //  Debug.Log(gameObject.name + " is too far" + " current: " + gameObject.transform.position+ " Spawn "+SpawnPoint);
            }
        }
        StartCoroutine(PatrolCorrection());
    }
    #endregion Navigation
   


}
