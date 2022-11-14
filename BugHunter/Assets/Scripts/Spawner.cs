using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
   Hound, DreadBomber, Golem, Worm, WalmartTorterra
}
public class Spawner : MonoBehaviour
{
    public EnemyType EnemySelection;
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
        SelectEnemy(EnemySelection);
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
        //this is the not flyweight way
        // yield return new WaitForSeconds(5.0f);
     
        //this is the flyweight way
        yield return SpawnTime;
       Instantiate(prefab[0], this.gameObject.transform).GetComponent<GroundAi>().SetTimes(lungeWait, lungeDuration, SwingDuration);

        StartCoroutine(HoundSpawn());
    }
    private IEnumerator DreadBomberSpawn()
    {
        //this is the not flyweight way
        // yield return new WaitForSeconds(5.0f);

        //this is the flyweight way
        yield return SpawnTime;
        Instantiate(prefab[1], this.gameObject.transform);

        StartCoroutine(DreadBomberSpawn());
    }
}
