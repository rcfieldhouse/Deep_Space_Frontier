using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] private WaitForSeconds lungeWait = new WaitForSeconds(0.25f);
    [SerializeField] private WaitForSeconds lungeDuration = new WaitForSeconds(1.5f);
    [SerializeField] private WaitForSeconds SwingDuration = new WaitForSeconds(0.75f);
    [SerializeField] private WaitForSeconds SpawnTimer = new WaitForSeconds(0.1f);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Iterator());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator Iterator()
    {
        //this is the not flyweight way
        // yield return new WaitForSeconds(5.0f);
        Debug.Log("meow");
        //this is the flyweight way
        yield return SpawnTimer;
       Instantiate(prefab, this.gameObject.transform).GetComponent<GroundAi>().SetTimes(lungeWait, lungeDuration, SwingDuration);

        StartCoroutine(Iterator());
    }
}
