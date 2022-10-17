using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    public WeaponInfo[] WeaponArray;
    public static AmmoManager instance;

    public void setAmmoCount()
    {
        for (int i = 0; i < 5; i++)
        {
            WeaponArray[i].SetMaxBullets();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
