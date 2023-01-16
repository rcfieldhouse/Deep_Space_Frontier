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

    //DetectionStuff
    [Range(0,50)] public float _SightRange, _AttackRange;
    public Vector3 SightRangeOffset, AttackAreaOffset;

    //Attack Stuff
    [Range(0, 50)] public int Attack_1_Damage, Attack_2_Damage;
    [Range(0,10)] public float Attack_1_Delay, Attack_2_Delay;

    //Death Stuff
    [Range(0.0f, 0.25f)] public float dissolveRate = 0.0125f, refreshRate = 0.02f;

    //Navigation Stuff
    private Vector3 WalkPoint, SpawnPoint;
    private bool WalkPointSet;
    private float WalkPointRange;
    [Range(0, 10)] public float WalkSpeed;

    void Awake()
    {
        Health=GetComponent<HealthSystem>();
        NavAgent=GetComponent<NavMeshAgent>();
        MeshRenderer=GetComponent<MeshRenderer>();

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
        Gizmos.DrawWireSphere(transform.position+AttackAreaOffset, _AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+SightRangeOffset, _SightRange);
    }
    public void HandleObjectDeath(GameObject context)
    {
        StartCoroutine(DissolveMeshEffect());
    }
    IEnumerator DissolveMeshEffect()
    {
        NavAgent.enabled = false;
        if (Materials.Length > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < Materials.Length; i++)
                {
                    Materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
            
            LootSpawner.instance.SprayLoot(transform);
            // LootSpawner.instance.SprayLoot(transform);
            // LootSpawner.instance.SprayLoot(transform);
            //this will need to be more elaborate later when we have anims and such, so i'm reworking it now ryan

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
