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

    [SerializeField] private WaitForSeconds lungeWait =new WaitForSeconds(0.5f);
    [SerializeField] private WaitForSeconds lungeDuration = new WaitForSeconds(1.5f);
    [SerializeField] private WaitForSeconds SwingDuration = new WaitForSeconds(0.75f);
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    HealthSystem health;
    //Attacking
    //damage is defined as either lunge damage or swing damage depending on the attack invoked
    public int lungeDamage=-10,SwingDamage,Damage;
    public bool alreadyAttacked=false,alreadyLunged=false;
    private bool PlayerInAttackBox=false,DamageDealt= false;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange,_isAttacking=false;

    private Rigidbody Rigidbody;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<HealthSystem>();
        Rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
         health = player.GetComponent<HealthSystem>();
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
        ScoreManager.instance.sChange(10); 
    }

    private void Update()
    {
        // LootSpawner.instance.Sprayoot(transform);
        //Check for sight and attack range
       
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (agent.enabled == true)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            if (!playerInAttackRange) SetLunged();
        }




        if (health != null && _isAttacking == true && DamageDealt == false&&PlayerInAttackBox==true)
        {
            // Call the damage function of that script, passing in our gunDamage variable
            health.ModifyHealth(Damage);
            DamageDealt = true;
        }
        //Debug.Log (Quaternion.Euler(agent.velocity));
    }
    private void SetLunged()
    {
        alreadyLunged = false;
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
        if (alreadyAttacked == false)
        {
            //Make sure enemy doesn't move
            Rigidbody.isKinematic = true;
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            if (alreadyLunged == false) StartCoroutine(LungeAttack());

            if (alreadyLunged == true&& alreadyAttacked == false) StartCoroutine(SwingAttack());

            alreadyAttacked = true;
        }


        if (alreadyAttacked==true)
        {
            alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private IEnumerator LungeAttack()
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
        Rigidbody.velocity = Vector3.Normalize(player.transform.position-gameObject.transform.position) * 10.0f+Vector3.up*2.0f;
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
        DamageDealt = false;
        Damage = 0;
        _isAttacking=false;
      // Debug.Log("reset");
        alreadyAttacked = false;
    }

    public void HandleObjectDeath(GameObject context)
    {
        LootSpawner.instance.SprayLoot(transform);
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
