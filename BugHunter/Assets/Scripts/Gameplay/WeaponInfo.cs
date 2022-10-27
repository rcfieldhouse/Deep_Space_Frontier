using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponInfo : MonoBehaviour
{
    //this script has data for weapons and is used to change data within classes that need the data from the weapons
    [Range(0, 1)][SerializeField] private float RecoilRotIntensity, RecoilOffsetIntensity,Weight;
    [Range(0,3)][SerializeField] private float RecoilTimer;

    [SerializeField] private int ammoInMag, maxAmmo, magSize = 1, reserveAmmo = 1;
    [Range(0, 5)][SerializeField] private float AdsZoomScale=0;
    public static Action<bool> maginfo,CanShoot;
   [Range(0, 10)] [SerializeField] private WaitForSeconds ReloadTimer= new WaitForSeconds(1.0f);
   [Range(0,50)][SerializeField] private float RecoilX, AimRecoilX;
   [Range(0,25)][SerializeField] private float RecoilY, AimRecoilY;
   [Range(0,10)][SerializeField] private float RecoilZ, AimRecoilZ;
   [Range(0,10)][SerializeField] private float snappiness;
   [Range(0,5)][SerializeField] private float returnSpeed;

    private bool _CanShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Reload += Reload;
        ammoInMag = magSize;
    }
    public void SetCanShoot(bool foo)
    {
        _CanShoot = foo;
    }
    public bool GetCanShoot()
    {
        return _CanShoot;
    }
    public void Update()
    {
        if (gameObject.activeInHierarchy == true)
        { 
            maginfo.Invoke(hasAmmo());
            CanShoot.Invoke(GetCanShoot());
        }
        Debug.Log(GetCanShoot());
    }
    public void OnDisable()
    {
        StopCoroutine(SetBulletCount(true));
    }
    public bool hasAmmo()
    {
        return ammoInMag > 0;
    }
    public Vector4 GetRecoilInfo()
    {
        return new Vector4(RecoilRotIntensity, RecoilOffsetIntensity, RecoilTimer, Weight);
       
    }
    //0 for hipfire 1 for ads
    public Vector3 GetCameraRecoilInfo(int num)
    {
        if (num == 0)
        {
            return new Vector3(RecoilX, RecoilY, RecoilZ);
        }
        if (num == 1)
        {
            return new Vector3(AimRecoilX, AimRecoilY, AimRecoilZ);
        }
        return Vector3.zero;

    }
    public Vector2 GetSnap()
    {
        return new Vector2(snappiness, returnSpeed);
    }
    public float GetADSZoom()
    {
        return AdsZoomScale;
    }
    public void Reload()
    {
        // Invoke(SetBulletCount, ReloadTimer);
      //  Invoke(nameof(SetBulletCount(true)), 1);
        //Setting to true reloads
        if(gameObject.activeInHierarchy==true)
        StartCoroutine( SetBulletCount(true));
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
    public IEnumerator SetBulletCount(bool var)
    {
        yield return ReloadTimer;
        //Debug.Log("reload");
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
    public void SetMaxBullets()
    {
        reserveAmmo = maxAmmo;
    }
}