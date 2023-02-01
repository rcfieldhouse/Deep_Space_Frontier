using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
   Hound, DreadBomber, Tick, Zephyr, Slime, 
}
public enum SpawnCondition
{
    TriggerEnter, ApplicationStart
}
public class Spawner : MonoBehaviour
{
    public SpawnCondition SpawnCondition;
    public List<EnemyType> EnemySelection;
    public List<int> NumEnemies;
    public Transform StartDestination;


    public List<GameObject> prefab;
    [Range(0, 30)] public float SpawnTimer=0.0f;

   [SerializeField] private WaitForSeconds lungeWait = new WaitForSeconds(0.25f);
    [SerializeField] private WaitForSeconds lungeDuration = new WaitForSeconds(1.5f);
    [SerializeField] private WaitForSeconds SwingDuration = new WaitForSeconds(0.75f);

 
     private WaitForSeconds SpawnTime = new WaitForSeconds(1.0f);
    // Start is called before the first frame update
    void Awake()
    {
        if (SpawnCondition == SpawnCondition.ApplicationStart)
        {
            SpawnTime = new WaitForSeconds(SpawnTimer);
            for (int i = 0; i < EnemySelection.Count; i++)
            {
                SelectEnemy(EnemySelection[i]);
            }
            if (gameObject.transform.GetChild(0) != null)
            {
                StartDestination = gameObject.transform.GetChild(0);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (SpawnCondition == SpawnCondition.TriggerEnter)
        {
            SpawnTime = new WaitForSeconds(SpawnTimer);
            for (int i = 0; i < EnemySelection.Count; i++)
            {
                SelectEnemy(EnemySelection[i]);
            }
            if (gameObject.transform.GetChild(0) != null)
            {
                StartDestination = gameObject.transform.GetChild(0);
            }
        }

    }
    // Update is called once per frame

    public void SelectEnemy(EnemyType TypeEnemy)
    {
        switch (TypeEnemy)
        {
            case EnemyType.Hound:
                Invoke(nameof(HoundSpawn), SpawnTimer);
                break;
            case EnemyType.DreadBomber:
                Invoke(nameof(DreadBomberSpawn), SpawnTimer);
                break;
   
        }
    }
    private void HoundSpawn()
    {
        if (NumEnemies[0] > 0) { 

           GameObject Hound = GameObject.Instantiate(prefab[0], gameObject.transform);   
           Hound.GetComponent<GroundAi>().SetTimes(lungeWait, lungeDuration, SwingDuration);
            if (StartDestination != null)
            {    
                Hound.GetComponent<GroundAi>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)
                {
                    Hound.GetComponent<GroundAi>().SetInitialPosition(StartDestination.position);
                }
            }       
        NumEnemies[0]--;
            Invoke(nameof(HoundSpawn), SpawnTimer);
        }
    }
    private void DreadBomberSpawn()
    {
        if (NumEnemies[1] > 0)
        {
            GameObject Enemy = GameObject.Instantiate(prefab[1], gameObject.transform);
            if (StartDestination != null)
            { 
                Enemy.GetComponent<AirAi>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)
                {
                    Enemy.GetComponent<AirAi>().SetInitialPosition(StartDestination.position);
                }
            }
            //go to next spawn
            NumEnemies[1]--;
            Invoke(nameof(HoundSpawn), SpawnTimer);
        }
    }
    
}
