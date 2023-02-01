using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthLoot : LFInterface
{ 
    public void Create(GameObject obj)
    {
        obj.AddComponent<HealthPickup>();
        obj.AddComponent<SphereCollider>().radius *= 10;
        obj.GetComponent<SphereCollider>().isTrigger = true;
        obj.AddComponent<LootMagnet>();
    }
}
internal class AmmoLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        obj.AddComponent<AmmoPickUp>();
        obj.AddComponent<SphereCollider>().radius *= 10;
        obj.GetComponent<SphereCollider>().isTrigger = true;
        obj.AddComponent<LootMagnet>();
    }
}
internal class UpgradeLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        obj.AddComponent<SphereCollider>().radius *=10;
        obj.GetComponent<SphereCollider>().isTrigger = true;
        obj.AddComponent<LootMagnet>();
        //obj.AddComponent<MaterialPickup>();
        //obj.GetComponent<Renderer>().materials[0].color = Color.green;
    }
}