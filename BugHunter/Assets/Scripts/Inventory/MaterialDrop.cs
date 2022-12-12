using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    public MaterialTypes Material; 
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LootSpawner.instance.DropMaterials(transform, (int)Material);
            Destroy(gameObject);
        }
    }
}
