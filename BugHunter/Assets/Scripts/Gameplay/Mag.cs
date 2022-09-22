using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    [SerializeField]private int ammoInMag, maxAmmo, magSize=1,reserveAmmo=1;

    // Start is called before the first frame update
    void Start()
    {
        ammoInMag = magSize;
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
                ammoInMag +=reserveAmmo;
                reserveAmmo = 0;
            }
               
        }
           
    }
}
