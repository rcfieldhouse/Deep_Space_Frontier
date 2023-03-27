using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMagnet : MonoBehaviour
{
    private GameObject Player=null;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Player = other.gameObject;
            Vector3 vec=Vector3.Normalize(Player.transform.position-transform.position);
            GetComponent<Rigidbody>().velocity = vec*8;
        }
    }
}
