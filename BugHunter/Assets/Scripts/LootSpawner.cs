using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public static LootSpawner instance;
    public GameObject prefab,gem;
    public Transform Transform;
    private float num;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public void SprayLoot(Transform transform)
    {
        num = Random.Range(0.0f, 100.0f);
        LFInterface loot= LootFactory.CreateLoot(LootType.Ammo); 

        if (num<33.3f)
             loot = LootFactory.CreateLoot(LootType.Health);
        else if (num>66.7f)
             loot = LootFactory.CreateLoot(LootType.UpgradeMats);
        Create(loot,transform);
    }
    void Create(LFInterface foo, Transform transform)
    {
        gem=Instantiate(prefab, transform.position, Quaternion.identity);
        foo.Create(gem);
    }
}
