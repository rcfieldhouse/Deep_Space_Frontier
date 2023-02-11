using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    enum EnemyType {
        Slime = 0,
        Tick = 3,
        Zephyr = 6,
        Bomber = 9
    };
    enum Rarity
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3
    };
    [Header("For Single Drops")]
    public int LootType = 0;

    [SerializeField] private EnemyType Enemy = EnemyType.Slime;
    [Seperator()]
    [SerializeField] private Rarity rarity = Rarity.Common;


    [Header("For Random Drops")]
    public bool RandomDrop = false;

    [Range(0, 100)] public float CommonDrop;

    [Range(0, 100)] public float UncommonDrop;

    [Range(0, 100)] public float RareDrop;


    HealthSystem Health;

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
            if (RandomDrop == false)
            {
                LootSpawner.instance.DropMaterials(transform, (int)Enemy + (int)rarity);
            }

            else if (RandomDrop == true)
            {
                DoRandomDrop();
            }
            //checks if the script is attached to any ai 
           if(GetComponentInParent<AI>()==null)
            Destroy(gameObject);
        }
    }

    private void DoRandomDrop()
    {
        float RnG = Random.Range(0, 100);

        if(RnG <= CommonDrop)
            LootSpawner.instance.DropMaterials(transform, (int)Enemy + 0);
        if (RnG <= UncommonDrop)
            LootSpawner.instance.DropMaterials(transform, (int)Enemy + 1);
        if (RnG <= RareDrop)
            LootSpawner.instance.DropMaterials(transform, (int)Enemy + 2);

    }
}
