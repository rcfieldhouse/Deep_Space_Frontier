using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthLoot : LFInterface
{
    public void Create(GameObject obj)
    {
       obj.AddComponent<HealthPickup>();
       obj.GetComponent<Renderer>().materials[0].color = Color.red;
    }
}
internal class AmmoLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        obj.AddComponent<AmmoPickUp>();
        obj.GetComponent<Renderer>().materials[0].color = Color.yellow;
    }
}
internal class UpgradeLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        obj.AddComponent<MaterialPickup>();
        obj.GetComponent<Renderer>().materials[0].color = Color.green;
    }
}