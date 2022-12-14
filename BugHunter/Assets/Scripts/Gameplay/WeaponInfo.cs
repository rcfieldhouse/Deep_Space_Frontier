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
   [Range(0, 10)] [SerializeField] public WaitForSeconds ReloadTimer= new WaitForSeconds(1.0f);
    [Range(0, 10)] public float _reloadTimer = 1.0f; 
    [Range(0,50)][SerializeField] private float RecoilX, AimRecoilX;
   [Range(0,25)][SerializeField] private float RecoilY, AimRecoilY;
   [Range(0,10)][SerializeField] private float RecoilZ, AimRecoilZ;
   [Range(0,10)][SerializeField] private float snappiness;
   [Range(0,5)][SerializeField] private float returnSpeed;

    private bool _CanShoot = true, _CanReload = false, _isReloading = false;
    // Start is called before the first frame update
    void Awake()
    {
        ReloadTimer = new WaitForSeconds(_reloadTimer);
        PlayerInput.Reload += Reload;
        ammoInMag = magSize;
    }
    private void OnDestroy()
    {
        PlayerInput.Reload -= Reload;
    }
    public bool GetIsReloading()
    {
        return _isReloading;
    }
    public void SetIsReloading(bool var)
    {
        _isReloading = var;
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
       
        if (ammoInMag <= 0) _CanShoot = false;

        if (ammoInMag == magSize)
            _CanReload = false;
        else _CanReload = true;

        if (gameObject.activeInHierarchy == true)
        {
            maginfo.Invoke(hasAmmo());
            CanShoot.Invoke(GetCanShoot());
        }
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
        if (_CanReload == true)
        {
            _CanShoot = false;
            if (gameObject.activeInHierarchy == true)
                StartCoroutine(SetBulletCount(true));

        }
       
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
    public bool GetCanReload()
    {
        return _CanReload;
    }
    public void SetMag(int Amount)
    {
        ammoInMag = Amount;
    }
    public void SetReserveAmmo(int Amount)
    {
        reserveAmmo = Amount;
        if (reserveAmmo > maxAmmo)
        {
            reserveAmmo = maxAmmo;
        }
    }
    public int GetMaxBullets()
    {
        return maxAmmo;
    }
    public IEnumerator SetBulletCount(bool var)
    {
        _isReloading = true;
           _CanReload = false;
        _CanShoot = false;
        gameObject.GetComponentInParent<ReloadGun>().SetIsReloading(true);
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
      
        _CanShoot = true;

        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponentInParent<ReloadGun>().SetIsReloading(false);
        _isReloading = false;

    }
    public void SetMaxBullets()
    {
        reserveAmmo = maxAmmo;
    }
}