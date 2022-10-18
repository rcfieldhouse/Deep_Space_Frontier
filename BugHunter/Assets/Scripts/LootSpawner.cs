using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var loot = LootFactory.CreateLoot(LootType.Ammo);
        Create(loot);   
    }
    void Create( LFInterface foo)
    {
        foo.Create();
    }
}
