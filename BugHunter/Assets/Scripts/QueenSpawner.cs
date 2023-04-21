using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class QueenSpawner : NetworkBehaviour
{

    public GameObject Prefab;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            GameObject Enemy = Instantiate(Prefab, transform.position , Quaternion.identity);
            Enemy.GetComponent<NetworkObject>().Spawn();

            Destroy(gameObject);
        }
    }
}
