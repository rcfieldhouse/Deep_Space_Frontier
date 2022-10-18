using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthLoot : LFInterface
{
    public void Create(GameObject obj)
    {
       obj.AddComponent<PickupItems>();
    }
}
internal class AmmoLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        Debug.Log("Amo");
    }
}
internal class UpgradeLoot : LFInterface
{
    public void Create(GameObject obj)
    {
        Debug.Log("Upgrade");
    }
}