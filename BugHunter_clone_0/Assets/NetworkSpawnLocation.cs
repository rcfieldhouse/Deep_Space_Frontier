using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class NetworkSpawnLocation : NetworkBehaviour
{
    public GameObject EnemyFightPrefab;
    public GameObject EnemySpawnPrefab;
    public override void OnNetworkSpawn()
    {
        transform.position = GameObject.Find("NetworkManager").transform.position;

        if(IsServer && SceneManager.GetActiveScene().name == "SampleScene")
        {
            Vector3 pos = new Vector3(508, 6, 490);
            Instantiate(EnemyFightPrefab, pos, Quaternion.identity);

            pos = new Vector3(832, 4, 932);
            Instantiate(EnemySpawnPrefab, pos, Quaternion.identity);
        }
    }
}
