using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    public Loot.Rarity rarity = 0;

    [Range(0, 10)] public List<int> NumOfDrops;

    HealthSystem Health;
    public bool RandomDrop = false;
    private void Awake()
    {
        Health = GetComponent<HealthSystem>();
        Health.OnObjectDeath += Kaboom;
    }
    private void OnDisable()
    {
        Health.OnObjectDeath -= Kaboom;
    }
    // Start is called before the first frame update
    private void Kaboom(GameObject context)
    {
        if (context == this.gameObject)
        {
            for (int i = 0; i < NumOfDrops.Count; i++)
            {
                for (int j = 0; j < NumOfDrops[i]; j++)

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
