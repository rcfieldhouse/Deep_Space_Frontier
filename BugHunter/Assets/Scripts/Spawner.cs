using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
   Hound, DreadBomber, Golem, Worm, WalmartTorterra
}
public class Spawner : MonoBehaviour
{
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
    void Start()
    {
        SpawnTime = new WaitForSeconds(SpawnTimer);
        for (int i =0; i<EnemySelection.Count; i++)
        {
            SelectEnemy(EnemySelection[i]);
        }
       if (gameObject.transform.GetChild(0) != null)
        {
            StartDestination = gameObject.transform.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SelectEnemy(EnemyType TypeEnemy)
    {
        switch (TypeEnemy)
        {
            case EnemyType.Hound:
                StartCoroutine(HoundSpawn());
                break;
            case EnemyType.DreadBomber:
                StartCoroutine(DreadBomberSpawn());
                break;
            case EnemyType.Golem:
                StartCoroutine(HoundSpawn());
                break;
            case EnemyType.Worm:
                StartCoroutine(HoundSpawn());
                break;
            case EnemyType.WalmartTorterra:
                StartCoroutine(HoundSpawn());
                break;
        }
    }
    private IEnumerator HoundSpawn()
    {
        if (NumEnemies[0] > 0) { 
        //this is the not flyweight way
        // yield return new WaitForSeconds(5.0f);
     
        //this is the flyweight way
        yield return SpawnTime;

       
           GameObject Hound = GameObject.Instantiate(prefab[0], this.gameObject.transform);
           Hound.GetComponent<GroundAi>().SetTimes(lungeWait, lungeDuration, SwingDuration);
           if (StartDestination != null) Hound.GetComponent<GroundAi>().SetInitialDestination(StartDestination.position);
        
        //go to next spawn
        NumEnemies[0]--;
        StartCoroutine(HoundSpawn());
        }
    }
    private IEnumerator DreadBomberSpawn()
    {

        if (NumEnemies[1] > 0)
        {

            //this is the flyweight way
            yield return SpawnTime;


            GameObject Enemy = GameObject.Instantiate(prefab[1], this.gameObject.transform);
            if (StartDestination != null)
            { 
                Enemy.GetComponent<AirAi>().SetInitialDestination(StartDestination.position);
            }
            //go to next spawn
            NumEnemies[1]--;
            StartCoroutine(DreadBomberSpawn());
        }
    }
    
}
