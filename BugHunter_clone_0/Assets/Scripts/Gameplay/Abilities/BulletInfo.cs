using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    [Range(0, -150)] public int StandardBulletDamage,CryoBulletDamage;
    [Range(0, 10)] public float CryoBulletFreezeTime;
    [Range(0, 5)] public float BurnTimeTotal, BurnTickInterval;
    [Range(0, -25)] public int DamageDoneAtTick;
    [Range(0, -100)] public int ElectricBulletDamage;
    [Range(0, 20)] public float ElectricTargetingRadius;
    [Range(0, 2)] public float ElectricBoltInterval;

    public Vector3[] Data;
    private void Awake()
    {
        Data = new Vector3[4];
        Data[0].x = StandardBulletDamage;
        Data[1].x = CryoBulletDamage;
        Data[1].y = CryoBulletFreezeTime;
        Data[2].x = DamageDoneAtTick;
        Data[2].y = BurnTimeTotal;
        Data[2].z = BurnTickInterval;
        Data[3].x = ElectricBulletDamage;
        Data[3].y = ElectricTargetingRadius;
        Data[3].z = ElectricBoltInterval;
    }
    public Vector3[] GetData()
    {
        return Data;
    }
}
