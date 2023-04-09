using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSpawner : MonoBehaviour
{

    public GameObject Prefab;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Instantiate(Prefab, transform.position , Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
