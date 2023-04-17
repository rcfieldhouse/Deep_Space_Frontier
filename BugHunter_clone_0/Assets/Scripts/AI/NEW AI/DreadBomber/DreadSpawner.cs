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
            if (!IsServer)
            {
                return;
            }

            Instantiate(Slime, transform.position + transform.rotation * Vector3.right, Quaternion.identity);
            Instantiate(Slime, transform.position + transform.rotation * Vector3.left, Quaternion.identity);

            Destroy(gameObject);
        }       
    }
}
