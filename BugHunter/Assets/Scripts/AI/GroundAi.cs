using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundAi : MonoBehaviour
{
    private NavMeshAgent agent;


    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private HealthSystem Health;

    [SerializeField] private WaitForSeconds lungeWait     ;
    [SerializeField] private WaitForSeconds lungeDuration ;
    [SerializeField] private WaitForSeconds SwingDuration ;
 
    //Patroling
    public Vector3 walkPoint;
    public Vector3 SpawnPoint;
    bool walkPointSet;
    public float walkPointRange;
    public HealthSystem health;
    private float dist=0; 
    private Vector3 Pos = Vector3.zero;
    public Vector2 LungeForce = new Vector2(15.0f, 6.0f);
    //Attacking
    //damage is defined as either lunge damage or swing damage depending on the attack invoked
    public int lungeDamage=-10,SwingDamage,Damage;
    public bool alreadyAttacked=false,alreadyLunged=false;
    public bool PlayerInAttackBox=false,DamageDealt= false;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange,_isAttacking=false;

  
    private void Awake()
    {
         if (SpawnPoint == Vector3.zero||SpawnPoint==null) SpawnPoint = gameObject.transform.position;


        if (lungeWait == null)
        {
            lungeWait = new WaitForSeconds(0.25f);
            lungeDuration = new WaitForSeconds(1.5f);
            SwingDuration = new WaitForSeconds(0.75f);
        }
      
        agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<HealthSystem>();
        player = GameObject.FindWithTag("Player").transform;
        health = player.GetComponent<HealthSystem>();
        StartCoroutine(AddHealthData());
        
        if (agent.isOnNavMesh == false)
            Debug.Log("NOOOOOO");
    }
    public void SetInitialPosition (Vector3 vector3)
    {
  
        SpawnPoint = vector3;
    }
    private IEnumerator AddHealthData()
    {
        yield return new WaitForEndOfFrame();
        player = GameObject.FindWithTag("Player").transform;

        health = player.GetComponent<HealthSystem>();
    }
    public void SetTimes(WaitForSeconds LW, WaitForSeconds LD, WaitForSeconds SD)
    {
        lungeWait = LW;
        lungeDuration = LD;
        SwingDuration =SD;
    }
    private void OnEnable()
    {
        Health.OnObjectDeath += HandleObjectDeath;
        StartCoroutine(PatrolCorrection());
    }
    private void OnDisable()
    {
       
        Health.OnObjectDeath -= HandleObjectDeath;
        //ScoreManager.instance.sChange(10); 
    }

    private void Update()
    {
        // LootSpawner.instance.Sprayoot(transform);
        //Check for sight and attack range
       // Debug.Log(agent.destination.ToString());
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (agent.enabled == true)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            if (!playerInAttackRange) SetLunged();
         
        }

       // Debug.Log(agent.velocity.magnitude);
       //if (agent.velocity.magnitude > 7.5f)
       //{
       //    walkPointSet = false;
       //}

        if (health != null && _isAttacking == true && DamageDealt == false&&PlayerInAttackBox==true)
        {
            // Call the damage function of that script, passing in our gunDamage variable
            health.ModifyHealth(Damage);
            DamageDealt = true;
        }
        //Debug.Log (Quaternion.Euler(agent.velocity));
    }

    public void SetInitialDestination(Vector3 vec)
    {
        walkPointSet = true;
        walkPoint = vec;
    }
    private IEnumerator PatrolCorrection()
    {
        if (agent.isOnNavMesh)
        {
            dist = agent.remainingDistance;
            Pos = gameObject.transform.position;
        }
        yield return new WaitForSeconds(2.0f);
        if (agent.isOnNavMesh)
        {
            if (Mathf.Abs(Pos.x - gameObject.transform.position.x) < 0.1f) 
            {

                walkPointSet = false;
            }
            if ((Mathf.Abs(gameObject.transform.position.x- SpawnPoint.x)>60.0f)|| (Mathf.Abs(gameObject.transform.position.y - SpawnPoint.y) > 60.0f))
            {
                agent.SetDestination(SpawnPoint);
              //  Debug.Log(gameObject.name + " is too far" + " current: " + gameObject.transform.position+ " Spawn "+SpawnPoint);
            }
        }
        StartCoroutine(PatrolCorrection());
        
    }
    private void SetLunged()
    {
        alreadyLunged = false;
    }
    private void Patroling()
    {
        if (!walkPointSet) { 
            SearchWalkPoint();
           
        }


        if (walkPointSet)
        {
            
            transform.LookAt(walkPoint);
            agent.SetDestination(walkPoint);
            
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
            SearchWalkPoint();
        }
           
    } 
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        if (Mathf.Abs(randomX) > 1 && Mathf.Abs(randomZ) > 1)
        {
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround) == true)
            {
                walkPointSet = true;
            }
            else SearchWalkPoint();
        }
 
           
    }

    private void ChasePlayer()
    {

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (alreadyAttacked == false)
        {
            Rigidbody Rigidbody = gameObject.AddComponent<Rigidbody>();
            //Make sure enemy doesn't move
            Rigidbody.isKinematic = true;
            agent.SetDestination(transform.position);
          
            transform.LookAt(player);
            if (alreadyLunged == false) StartCoroutine(LungeAttack(Rigidbody));

            if (alreadyLunged == true&& alreadyAttacked == false) StartCoroutine(SwingAttack());

            alreadyAttacked = true;
        }


        if (alreadyAttacked==true)
        {
            alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private IEnumerator LungeAttack(Rigidbody Rigidbody)
    {
        alreadyAttacked = true;
        alreadyLunged = true;
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        yield return lungeWait;
        transform.LookAt(player);
        Rigidbody.isKinematic = false;
        _isAttacking = true;
        Damage = lungeDamage;
        Rigidbody.velocity = Vector3.Normalize(player.transform.position-gameObject.transform.position) * LungeForce.x + Vector3.up * LungeForce.y;
        yield return lungeDuration;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        ResetAttack();
    }

    private IEnumerator SwingAttack()
    {
        _isAttacking = true;
        Damage = SwingDamage;
        transform.LookAt(player);
      //  Debug.Log("they do be swinging");
        yield return SwingDuration;
        ResetAttack();
    }

    private void ResetAttack()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
        agent.enabled = true;
        DamageDealt = false;
        Damage = 0;
        _isAttacking=false;
      // Debug.Log("reset");
        alreadyAttacked = false;
    }

    public void HandleObjectDeath(GameObject context)
    {
        LootSpawner.instance.SprayLoot(transform);
       // LootSpawner.instance.SprayLoot(transform);
       // LootSpawner.instance.SprayLoot(transform);
        //this will need to be more elaborate later when we have anims and such, so i'm reworking it now ryan

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInAttackBox = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInAttackBox = true;
        }
        // if (other.tag == "Player"&&_isAttacking==true)
        // {
        //     HealthSystem health = other.GetComponent<HealthSystem>();
        //
        //     // If there was a health script attached
        //     if (health != null)
        //     {
        //         // Call the damage function of that script, passing in our gunDamage variable
        //         health.ModifyHealth(Damage);
        //     }
        // }
    }
}
