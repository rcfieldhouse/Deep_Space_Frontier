using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthLoot : LFInterface
{ 
    public void Create(GameObject obj)
    {
       obj.AddComponent<HealthPickup>();
    }
}
internal class AmmoLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        obj.AddComponent<AmmoPickUp>();
      
    }
}
internal class UpgradeLoot : LFInterface
{
    public void Create(GameObject obj)
    {
      //obj.AddComponent<MaterialPickup>();
      //obj.GetComponent<Renderer>().materials[0].color = Color.green;
    }
}