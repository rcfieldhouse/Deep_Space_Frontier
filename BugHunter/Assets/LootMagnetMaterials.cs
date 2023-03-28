using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMagnetMaterials : MonoBehaviour
{
    private GameObject Player = null;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"||other.tag == "GodOrb")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Player = other.gameObject;
            Vector3 vec = Vector3.Normalize(Player.transform.position - transform.position);
            GetComponent<Rigidbody>().velocity = vec * 8;
        }
    }
    private void Awake()
    {
        Invoke(nameof(KillThis), 30.0f);
    }
    void KillThis()
    {
        Destroy(gameObject);
    }
}
