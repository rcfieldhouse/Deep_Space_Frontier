using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AirAi : MonoBehaviour
{
    // Start is called before the first frame update


    private NavMeshAgent agent;

    public GameObject projectile; 
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private HealthSystem Health;
    private float dist = 0;
    private Vector3 Pos = Vector3.zero;
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
        StartCoroutine(PlsWork());
        Health.OnObjectDeath += HandleObjectDeath;
    }
    private void OnDisable()
    {
        Health.OnObjectDeath -= HandleObjectDeath;
    }
    public void SetInitialDestination(Vector3 vec)
    {
        walkPointSet = true;
        walkPoint = vec;
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (agent.enabled == true)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
       
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
        if (distanceToWalkPoint.magnitude < 2f)
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

    private IEnumerator PlsWork()
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
        }
        StartCoroutine(PlsWork());

    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

         //Attack 
          Rigidbody rb = Instantiate(projectile, transform.position+Vector3.up*3+transform.rotation*Vector3.forward*2, Quaternion.identity).GetComponent<Rigidbody>();
          rb.AddForce(Vector3.Normalize(player.transform.position-(transform.position + Vector3.up * 3)) * 10f, ForceMode.Impulse);
          
     
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
