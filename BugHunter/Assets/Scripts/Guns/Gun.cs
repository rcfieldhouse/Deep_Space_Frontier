using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    //variables that all guns will be using
    [Range(0, -50)]public int Damage = -25;
    [Range(0, 2)] public float FireRate = 0.25f;
    [Range(0, 250)] public float WeaponRange = 50f;
    [Range(0, 200)] public float HitForce = 100f;
    [Range(0, 3)] public float CritMultiplier = 1.0f;
    public Vector2 Hipfire_Spread, ADS_Spread;
    [HideInInspector] public WeaponInfo info;
    [HideInInspector] public Transform GunEnd;
    [HideInInspector] public LineRenderer LazerLine;
    [HideInInspector] public ParticleSystem MuzzleFlash;
    [HideInInspector] public GameObject HitMarkers;
    [HideInInspector] public Camera Camera;
    [HideInInspector] public float NextFire;
    [HideInInspector] public bool _IsAiming=false;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.15f);

    public abstract void Shoot();

    private void Awake()
    {
        HitMarkers = transform.parent.parent.parent.parent.GetComponentInChildren<GUIHolder>().HitMarkers;
        info = GetComponent<WeaponInfo>();
        LazerLine = GetComponent<LineRenderer>();
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        Camera = transform.parent.GetComponentInParent<Camera>();
        GunEnd = MuzzleFlash.transform;
        PlayerInput.Shoot += Shoot;
        PlayerInput.ADS += AIM;
    }
    private void OnDestroy()
    {
        PlayerInput.Shoot -= Shoot;
        PlayerInput.ADS -= AIM;
    }
    public void AIM(bool aiming)
    {
        if (gameObject.activeInHierarchy == true)
            _IsAiming = aiming;
    }
    private void Update()
    {
        if (Time.time < NextFire && gameObject.activeInHierarchy == true)
            info.SetCanShoot(false);
        else if (Time.time > NextFire && gameObject.activeInHierarchy == true&&info.GetMag()>0)
            info.SetCanShoot(true);
    }
    public void DoDamage(HealthSystem Health, bool _IsCrit)
    {    
        float DamageX = 1;
        if (_IsCrit) DamageX = CritMultiplier;
        if (Health)
        {
            StartCoroutine(HitMarkerEffect(_IsCrit));
            Health.ModifyHealth((int)(Damage * DamageX));
        }
    }
    public HealthSystem FindHealth(Collider collider)
    {
        if (collider.tag == "BossColliderHolder")
          return FindBossHealth(collider.gameObject);               
        else
          return collider.GetComponent<HealthSystem>();
        
    }
    public HealthSystem FindBossHealth(GameObject obj)
    {
        if (obj.tag == "Boss")
            return obj.GetComponent<HealthSystem>();    
        else 
            return FindBossHealth(obj.transform.parent.gameObject);     
    }
    public IEnumerator HitMarkerEffect(bool HitType)
    {
        int var = 0;
        if (HitType == true) var = 1;
        Debug.Log(var);
        //0 = normal 1 = critical
        HitMarkers.transform.GetChild(var).gameObject.SetActive(true);
        yield return shotDuration;
        HitMarkers.transform.GetChild(var).gameObject.SetActive(false);
    }
    public IEnumerator ShotEffect()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Projectiles/Gunshot_Light");
        MuzzleFlash.Play();
        LazerLine.enabled = true;
        LazerLine.SetPosition(0, GunEnd.position);
        yield return shotDuration;
        LazerLine.enabled = false;
    }
}
