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
    public int[] NumSpawns;

    public List<GameObject> prefab;
    [Range(0, 30)] public float SpawnTimer=0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        NumSpawns = new int[5];
        if (SpawnCondition == SpawnCondition.ApplicationStart)
        {
            for (int i = 0; i < EnemySelection.Count; i++)
            {
                SelectEnemy(EnemySelection[i], i);
            }
            if (gameObject.transform.GetChild(0) != null)
            {
                StartDestination = gameObject.transform.GetChild(0);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (SpawnCondition == SpawnCondition.TriggerEnter)
        {
            for (int i = 0; i < EnemySelection.Count; i++)
            {
                SelectEnemy(EnemySelection[i],i);
            }
            if (gameObject.transform.GetChild(0) != null)
            {
                StartDestination = gameObject.transform.GetChild(0);
            }
        }

    }
    // Update is called once per frame

    public void SelectEnemy(EnemyType TypeEnemy,int index)
    {
        switch (TypeEnemy)
        {
            case EnemyType.Hound:
                NumSpawns[0] = NumEnemies[index];
                Invoke(nameof(HoundSpawn), SpawnTimer);
                break;
            case EnemyType.DreadBomber:
                NumSpawns[1] = NumEnemies[index];
                Invoke(nameof(DreadBomberSpawn), SpawnTimer);
                break;
            case EnemyType.Tick:
                NumSpawns[2] = NumEnemies[index];
                Invoke(nameof(TickSpawn), SpawnTimer);
                break;
            case EnemyType.Zephyr:
                NumSpawns[3] = NumEnemies[index];
                Invoke(nameof(ZephyrSpawn), SpawnTimer);
                break;
            case EnemyType.Slime:
                NumSpawns[4] = NumEnemies[index];
                Invoke(nameof(SlimeSpawn), SpawnTimer);
                break;

        }
        NumEnemies[index] = 0;
    }
    private void HoundSpawn()
    {
        if (NumSpawns[0] <= 0)
            return;

             GameObject Hound = GameObject.Instantiate(prefab[0], gameObject.transform);   
            //  Hound.GetComponent<GroundAi>().SetTimes(lungeWait, lungeDuration, SwingDuration);
            if (StartDestination != null)
            {    
                Hound.GetComponent<GroundAi>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)
                    Hound.GetComponent<GroundAi>().SetInitialPosition(StartDestination.position);
            }
            NumSpawns[0]--;
            Invoke(nameof(HoundSpawn), SpawnTimer);
        
    }
    private void DreadBomberSpawn()
    {
        if (NumSpawns[1] <= 0)
            return;

        GameObject Enemy = GameObject.Instantiate(prefab[1], gameObject.transform);
            if (StartDestination != null)
            { 
                Enemy.GetComponent<DreadBomber>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)
                    Enemy.GetComponent<DreadBomber>().SetInitialPosition(StartDestination.position);
            }
            //go to next spawn
            NumSpawns[1]--;
            Invoke(nameof(DreadBomberSpawn), SpawnTimer);
    }
    private void TickSpawn()
    {
        if (NumSpawns[2] <= 0)
            return;

        GameObject Enemy = GameObject.Instantiate(prefab[2], gameObject.transform);
            if (StartDestination != null)
            {
                Enemy.GetComponent<Tick>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)               
                  Enemy.GetComponent<Tick>().SetInitialPosition(StartDestination.position);            
            }
            //go to next spawn
            NumSpawns[2]--;
            Invoke(nameof(TickSpawn), SpawnTimer);      
    }
    private void ZephyrSpawn()
    {
        if (NumSpawns[3] <= 0)
            return;

        GameObject Enemy = GameObject.Instantiate(prefab[3], gameObject.transform);
            if (StartDestination != null)
            {
                Enemy.GetComponent<Beetle>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)
                  Enemy.GetComponent<Beetle>().SetInitialPosition(StartDestination.position);
              
            }
            //go to next spawn
            NumSpawns[3]--;
            Invoke(nameof(ZephyrSpawn), SpawnTimer);     
    }
    private void SlimeSpawn()
    {
        if (NumSpawns[4] <= 0)
            return;

        GameObject Enemy = GameObject.Instantiate(prefab[4], gameObject.transform);
            if (StartDestination != null)
            {
                Enemy.GetComponent<Slime>().SetInitialDestination(StartDestination.position);
                if (StartDestination.position != Vector3.zero)
                    Enemy.GetComponent<Slime>().SetInitialPosition(StartDestination.position);
              
            }
            //go to next spawn
            NumSpawns[4]--;
            Invoke(nameof(SlimeSpawn), SpawnTimer);
    }

}
