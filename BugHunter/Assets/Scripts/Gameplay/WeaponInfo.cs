using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponInfo : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float RecoilRotIntensity, RecoilOffsetIntensity,Weight;
    [Range(0,3)][SerializeField] private float RecoilTimer;
    [SerializeField] private int ammoInMag, maxAmmo, magSize = 1, reserveAmmo = 1;
    public static Action<bool> maginfo;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Reload += Reload;
        ammoInMag = magSize;
    }
    public void Update()
    {
        maginfo.Invoke(hasAmmo());
    }
    public bool hasAmmo()
    {
        return ammoInMag > 0;
    }
    public Vector4 GetRecoilInfo()
    {
        return new Vector4(RecoilRotIntensity, RecoilOffsetIntensity, RecoilTimer, Weight);
       
    }
    public void Reload()
    {
        //Setting to true reloads
        SetBulletCount(true);
    }
    // Update is called once per frame
    //alter mag to subtract a bullet or fill it full on reload 
    public void SetBulletCount()
    {
        if (ammoInMag > 0)
            ammoInMag--;
    }
    public int GetMag()
    {
        return ammoInMag;
    }
    public int GetReserveAmmo()
    {
        return reserveAmmo;
    }
    public void SetBulletCount(bool var)
    {
        if (var)
        {
            if (reserveAmmo > magSize - ammoInMag)
            {
                reserveAmmo -= magSize - ammoInMag;
                ammoInMag = magSize;
            }
            else
            {
                ammoInMag += reserveAmmo;
                reserveAmmo = 0;
            }

        }

    }
}