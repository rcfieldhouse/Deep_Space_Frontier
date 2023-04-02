using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    [HideInInspector] public Animator AI_Animator;
    [HideInInspector] public HealthSystem Health;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public MeshRenderer MeshRenderer;
    [HideInInspector] public SkinnedMeshRenderer SkinnedMeshRenderer;
    [HideInInspector] public Material[] Materials;
    [HideInInspector] public GameObject Target;

    [Header("Sniper Materials")]
    public Material iceMaterial;

    public Material fireMaterial;
    public Material electricMaterial;

    [Header("Hurt Material")]
    public Material hurtMaterial;

    private bool _IsDead = false;

    [Header("Detection Stuff")]
    //DetectionStuff
    [Range(0, 100)] public float _SightRange = 10;
    [Range(0, 100)] public float _AttackRange = 2,_Attack2_Range;
    public Vector3 SightRangeOffset, AttackAreaOffset,_Attack2AreaOffset;

    [Header("Attack Stuff")]
    [Seperator()]
    //Attack Stuff
    [Range(0, -50)] public int Attack_1_Damage, Attack_2_Damage;
    [Range(0, 10)] public float Attack_1_Delay, Attack_2_Delay;
    [HideInInspector] public bool CanAttack=true,HasAttacked=false,IsSecondaryAttack=false;

    [Header("Death and Damage Stuff")]
    [Seperator()]
    //Death and Damage Stuff
    [Range(0.0f, 0.25f)] public float dissolveRate = 0.0125f;
    [Range(0.0f, 0.25f)] public float refreshRate = 0.02f;
    [Range(0, 8)] public int NumDrops = 0;
    [Range(0,100)] public int HitStunDamageRequirement = 0;
    [Range(0, 5)] public float HitStunTimeRequirement = 0,HitStunTime=0;
    private float DamageTakenTime=0;
    private int DamageTaken = 0;
    [HideInInspector] public bool _IsHitStunned = false;

    [Header("Navigation Stuff")]
    [Seperator()]
    //Navigation Stuff
    //Dante, Before you ask, Serpentine is the thing i use to make the enemy go side to side
   [HideInInspector] public Vector3 StartEvasionLocation,DistanceTravelled = Vector3.zero;
   [HideInInspector] public float Serpentine = 0.0f;
    [Range(0, 10)] public float EvasionIntensity=0, EvasionRecalculationPeriod=0;
    public LayerMask WhatIsGround,WhatIsPlayer;
   [HideInInspector] public Vector3 WalkPoint, SpawnPoint,Pos;
   [HideInInspector] public bool WalkPointSet=false,_IsChasing=false;
    [Range(0, 100)] public float WalkPointRange,WalkSpeed;

    private Material[] cachedMaterials;
    private Renderer Renderer;

    FMODUnity.StudioEventEmitter DeathSound;

    #region MonoBehaviour
    public virtual void Awake()
    {
        AI_Animator = GetComponentInChildren<Animator>();
        Health = GetComponentInChildren<HealthSystem>();
        Health.OnObjectDeathT += HandleObjectDeath;
        Health.OnHealthPercentChanged += HandleObjectHit;
        NavAgent = GetComponent<NavMeshAgent>();
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        Renderer = GetComponentInChildren<Renderer>();

        DeathSound = GetComponent<FMODUnity.StudioEventEmitter>();

        cachedMaterials = Renderer.materials;

        if (HitStunDamageRequirement!=0)
        Health.OnTakeDamage += StaggerMechanic;
        if (NavAgent.isOnNavMesh == false)
            Debug.Log("NOOOOOO");
        if (MeshRenderer != null)
            Materials = MeshRenderer.materials;
        else if (SkinnedMeshRenderer != null)
            Materials = SkinnedMeshRenderer.materials;

        NavAgent.speed = WalkSpeed;
        CanAttack = true;
        //essentially makes them not dumb
        PatrolCorrection();
    }
    public virtual void Update()
    {
        if (_IsDead)
            return;

        bool playerInSightRange = Physics.CheckSphere(transform.position+ transform.rotation* SightRangeOffset, _SightRange, WhatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position + transform.rotation * AttackAreaOffset, _AttackRange, WhatIsPlayer);
        bool playerInAttackRange2 = Physics.CheckSphere(transform.position + transform.rotation * _Attack2AreaOffset, _Attack2_Range, WhatIsPlayer);
        //these functions can be found in the navigation reigon
        if (NavAgent.enabled == true && _IsHitStunned==false)
        {
            IsSecondaryAttack = playerInAttackRange2;   
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer(Target);
    
            //if (!playerInAttackRange) SetLunged();
        }
    }
    public void OnDisable()
    {
        Health.OnObjectDeathT -= HandleObjectDeath;

        Health.OnHealthPercentChanged -= HandleObjectHit;
        Health.OnTakeDamage -= StaggerMechanic;
        //ScoreManager.instance.sChange(10); 
    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position +transform.rotation*AttackAreaOffset, _AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * SightRangeOffset, _SightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, WalkPointRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * _Attack2AreaOffset, _Attack2_Range);
    }

    #endregion MonoBehaviour

    #region TakeDamage
    public void HandleObjectHit(float Hit)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<VFX_ID>() != null)
                transform.GetChild(i).gameObject.GetComponent<VFX_ID>().gameObject.SetActive(true);
        }

        var mats = new Material[Renderer.materials.Length];
        for (var j = 0; j < Renderer.materials.Length; j++)
        {
            mats[j] = hurtMaterial;
        }
        Renderer.materials = mats;
        StartCoroutine(hurtTimer());
    }

    IEnumerator hurtTimer()
    {
        yield return  new WaitForSeconds(0.2f);
        Renderer.materials = cachedMaterials;
    }

    public void StaggerMechanic(int Damage)
    {
        if((Time.time - DamageTakenTime) < HitStunTimeRequirement)
        {       
            DamageTaken -= Damage;
            if (DamageTaken >= HitStunDamageRequirement&&_IsHitStunned==false)
                Stagger();
        }
        else
        {
            DamageTakenTime = Time.time;
            DamageTaken = -Damage;
            StaggerMechanic(0);
        }      
    }
    public void Stagger()
    {
        AI_Animator.SetBool("_IsHurt", true);
        NavAgent.SetDestination(transform.position);
        _IsHitStunned = true;
        NavAgent.enabled = false;
        Invoke(nameof(ResetStagger), HitStunTime);
    }
    private void ResetStagger()
    {
        AI_Animator.SetBool("_IsHurt", false);
        DamageTaken = 0;
        DamageTakenTime = 0;
        NavAgent.enabled = true;
        _IsHitStunned = false;
    }
    public void HandleObjectDeath(Transform context)
    {
    
        if (GetComponent<Tick>())
        {           
            GetComponentInChildren<BoxCollider>().enabled = false;
            GetComponentInChildren<SphereCollider>().enabled = false;
            
        }
        if (GetComponent<DreadBomber>())
        {
            GetComponentInChildren<SphereCollider>().enabled = false;
            GetComponentInChildren<SphereCollider>().enabled = false;
        }
        if (GetComponent<Slime>())
        {
            GetComponentInChildren<CapsuleCollider>().enabled = false;
        }
        if (GetComponent<Beetle>())
        {

        }
        if (GetComponent<Queen>())
        {
            GameManager.instance.SceneChange("Hub");
        }
        StartCoroutine(DissolveMeshEffect());
        _IsDead = true;
        DeathSound.Play();

    }
    IEnumerator DissolveMeshEffect()
    {
        //GetComponentInChildren<BoxCollider>().enabled = false;
        AI_Animator.SetBool("_IsDead", true);
        float DropRate = 1.0f / (float)NumDrops;
        float DropIndexer = 0.0f;
        NavAgent.enabled = false;
        if (Materials.Length > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_DissolveAmount") < 1)
            {
                NavAgent.enabled = false;
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
    public GameObject FindClosestPlayer()
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

    public virtual void Patroling()
    {
        _IsChasing = false;
        //if (Target == null)
        //    Target = FindClosestPlayer();
        ////play Dante.sound.ogg AI idle
        if (!WalkPointSet)
            SearchWalkPoint();

        if (WalkPointSet)
        {
           // transform.Rotate(WalkPoint);

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
    public virtual void ChasePlayer()
    {
        _IsChasing = true;
       // Debug.Log(NavAgent.remainingDistance+" "+WalkPointSet);

        if (Target==null)
          Target = FindClosestPlayer();


        if (WalkPointSet == false)
        {
         
            Serpentine = Random.Range(-EvasionIntensity, EvasionIntensity);
            StartEvasionLocation = transform.position;
            WalkPointSet = true;    
            NavAgent.SetDestination(transform.position + WalkPoint + transform.rotation * new Vector3(Serpentine, 0.0f, 0.0f));
        }
        DistanceTravelled = transform.position - StartEvasionLocation;
        AI_Animator.SetBool("_IsMoving", true);
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

        if (attackDirectly==true)
            NavAgent.SetDestination(Target.transform.position);
        
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
   private void PatrolCorrection()
   {
       if (NavAgent.isOnNavMesh)
            Pos = gameObject.transform.position;

        Invoke(nameof(CheckIfStuck),2.0f);
      // yield return new WaitForSeconds(2.0f);
     
     //  StartCoroutine(PatrolCorrection());
   }

    private void CheckIfStuck()
    {
     
        if (NavAgent.isOnNavMesh&&_IsChasing==false)
        {
            if (Mathf.Abs(Pos.x - gameObject.transform.position.x) < 0.1f)
            {
                WalkPointSet = false;
            }
            if ((Mathf.Abs(gameObject.transform.position.x - SpawnPoint.x) > 60.0f) || (Mathf.Abs(gameObject.transform.position.y - SpawnPoint.y) > 60.0f))
            {
             //   NavAgent.SetDestination(SpawnPoint);
            }
        }
        PatrolCorrection();
    }
    #endregion Navigation
   


}
