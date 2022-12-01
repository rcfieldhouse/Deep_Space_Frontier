using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    bool Deployed = false;
    public List<GameObject> SpawnPoints; 
    public List<GameObject> LootDrops; 
    // Start is called before the first frame update
    private void Start()
    {
      
        for (int i = 0; i<transform.childCount; i++)
        {
            SpawnPoints.Add(transform.GetChild(i).gameObject);
        }
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Pause();
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        if (Deployed == false)
        {
            DeployLoot();
        }
        
        Deployed = true;
    }
    // Update is called once per frame
  
    void DeployLoot()
    {
        if (Deployed == false)
        {

      
            for (int i = 0; i < LootDrops.Count; i++)
             {
            for (int j=0; j < SpawnPoints.Count; j++)
            {
                Debug.Log("Spawned loot " + i+ " at " + j);
                Instantiate(LootDrops[i], SpawnPoints[j].transform);
            }
             }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Drop();
        }

    }
    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponentInChildren<ParticleSystem>().Play();
    }
}
