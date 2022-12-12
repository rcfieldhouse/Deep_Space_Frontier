using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    public List<MaterialTypes> MaterialAtThisIndex;
    [Range(0, 10)] public List<int> NumMaterialsAtThisIndex;
    public bool RandomDrop = false;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i=0; i < NumMaterialsAtThisIndex.Count; i++)
            {
                for (int j=0;j< NumMaterialsAtThisIndex[i];j++)
                    if(RandomDrop==false)
                LootSpawner.instance.DropMaterials(transform, (int)MaterialAtThisIndex[i]);
                else if (RandomDrop == true)
                    {
                      
                        LootSpawner.instance.DropMaterials(transform, (int)Random.Range(0, 6));
                    }
                        
            }
           
            Destroy(gameObject);
        }
    }
}
