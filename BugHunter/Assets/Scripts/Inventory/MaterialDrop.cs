using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    public Loot.Rarity rarity = 0;

    [Range(0, 10)] public List<int> NumOfDrops;

    public bool RandomDrop = false;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i=0; i < NumOfDrops.Count; i++)
            {
                for (int j=0;j< NumOfDrops[i]; j++)

                    if (RandomDrop == false)
                    {
                        LootSpawner.instance.DropMaterials(transform, (int)rarity);
                    }
    
                    else if (RandomDrop == true)
                    {                      
                        LootSpawner.instance.DropMaterials(transform, Random.Range(0, 4));
                    }
                        
            }
           
            Destroy(gameObject);
        }
    }
}
