using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DreadSpawner : NetworkBehaviour
{
    public GameObject Slime;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {

            GameObject Enemy = Instantiate(Slime, transform.position + transform.rotation * Vector3.right, Quaternion.identity);
            Enemy.GetComponent<NetworkObject>().Spawn();

            Enemy = Instantiate(Slime, transform.position + transform.rotation * Vector3.left, Quaternion.identity);
            Enemy.GetComponent<NetworkObject>().Spawn();


            Destroy(gameObject);
        }       
    }
}
