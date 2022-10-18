using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject prefab,gem;
    public Transform Transform;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.spawnLoot += Sprayoot;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Sprayoot()
    {

        var loot = LootFactory.CreateLoot(LootType.Health);
        Create(loot);
    }
    void Create(LFInterface foo)
    {
        gem=Instantiate(prefab, Transform.position, Quaternion.identity);
        foo.Create(gem);
    }
}
