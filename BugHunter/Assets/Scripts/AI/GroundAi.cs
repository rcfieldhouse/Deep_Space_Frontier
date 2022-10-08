
using UnityEngine;
using UnityEngine.AI;

public class GroundAi : MonoBehaviour
{
    private NavMeshAgent agent;


    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private HealthSystem Health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<HealthSystem>();
        player = GameObject.FindWithTag("Player").transform;

        if (agent.isOnNavMesh == false)
            Debug.Log("NOOOOOO");
    }
    private void OnEnable()
    {
        Health.OnObjectDeath += HandleObjectDeath;
    }
    private void OnDisable()
    {
        Health.OnObjectDeath -= HandleObjectDeath;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        //Debug.Log (Quaternion.Euler(agent.velocity));
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            transform.LookAt(walkPoint);
            agent.SetDestination(walkPoint);
        }
            

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

  private void AttackPlayer()
   {
       //Make sure enemy doesn't move
       agent.SetDestination(transform.position);
    
       transform.LookAt(player);
    
       if (!alreadyAttacked)
       {

           ///Attack code here
         //  Rigidbody rb = Instantiate(projectile, projectilePos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
         //  rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
         //  rb.AddForce(transform.up * 8f, ForceMode.Impulse);
           ///End of attack code
    
           alreadyAttacked = true;
           Invoke(nameof(ResetAttack), timeBetweenAttacks);
       }
   }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void HandleObjectDeath(GameObject context)
    {
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

}
