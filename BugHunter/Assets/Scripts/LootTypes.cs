using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthLoot : LFInterface
{
    public void Create()
    {
        Debug.Log("Health");
    }
}
internal class AmmoLoot : LFInterface
{
    public void Create()
    {
        Debug.Log("Amo");
    }
}
internal class UpgradeLoot : LFInterface
{
    public void Create()
    {
        Debug.Log("Upgrade");
    }
}