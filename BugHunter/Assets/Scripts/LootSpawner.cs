using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public static LootSpawner instance;
    public GameObject prefab,gem;
    public Transform Transform;
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

        var loot = LootFactory.CreateLoot(LootType.Health);
        Create(loot,transform);
    }
    void Create(LFInterface foo, Transform transform)
    {
        gem=Instantiate(prefab, transform.position, Quaternion.identity);
        foo.Create(gem);
    }
}
