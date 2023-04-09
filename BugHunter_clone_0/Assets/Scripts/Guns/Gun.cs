using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IDataPersistence
{
    public int Level = 0;
    //variables that all guns will be using
    [Range(0, -50)]public int Damage = -25;
    [Range(0, 2)] public float FireRate = 0.25f;
    [Range(0, 250)] public float WeaponRange = 50f;
    [Range(0, 200)] public float HitForce = 100f;
    [Range(0, 3)] public float CritMultiplier = 1.0f;
    [Range(0, 1)] public float ADS_Accuracy;
    public Vector2 ShotSpread;
    public Transform CasingEjectPoint;
    public GameObject AmmoCasingPrefab;
    [HideInInspector] public WeaponInfo info;
    [HideInInspector] public Transform GunEnd;
    [HideInInspector] public LineRenderer LazerLine;
    [HideInInspector] public ParticleSystem MuzzleFlash;
    [HideInInspector] public GameObject HitMarkers;
    [HideInInspector] public Camera Camera;
    [HideInInspector] public float NextFire;
    [HideInInspector] public bool _IsAiming=false,_IsSprinting=false;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.15f);
    [HideInInspector] public PlayerInput Player;
    public int PrimaryWeaponLvl = 0, SecondaryWeaponLvl = 0;
    private bool UpdatedLevel = false;
    public virtual void Shoot() 
    {
        if (info.GetCanReload() == true && info.GetMag() <= 0&&info._isReloading==false)
        {
            GetComponentInParent<ReloadGun>().Reload();
            info.Reload();
            info._isReloading = true;
        }
          
    }
    public float CalculateWeaponDamageFalloff(float Distance)
    {
        if (Distance <= WeaponRange)
            return 1;

        for (int i = 0; i < 9; i++)
        {
            //Debug.Log(((float)i / 3.0f)*WeaponRange);
            if (Distance < (WeaponRange + (((i) / 3.0f) * WeaponRange)))
            {
                Debug.Log("Distance was " + Distance + " it smaller than " + (WeaponRange + (((i) / 3.0f) * WeaponRange)) + "Damage fallof : " + (1.0f-(float)i / 9.0f));
                return (1.0f - (float)i / 9.0f);
            }
         }
        return 0;
  
           
      
    }
    public void SetIsSprinting(bool var)
    {
        _IsSprinting = var;
    }
    [SerializeField] FMODUnity.EventReference shootSound;
    public void UpgradeWeapon()
    {
        if(Level<3)
        Level++;
        WeaponUpgrades(Level);
    }
    public abstract void WeaponUpgrades(int lvl);
    
    private void Awake()
    {
        Player = transform.parent.parent.parent.parent.GetChild(0).GetComponent<PlayerInput>();
        HitMarkers = transform.parent.parent.parent.parent.GetComponentInChildren<GUIHolder>().HitMarkers;
        info = GetComponent<WeaponInfo>();
        LazerLine = GetComponent<LineRenderer>();
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        Camera = transform.parent.GetComponentInParent<Camera>();
        GunEnd = MuzzleFlash.transform;
        Player.Sprinting += SetIsSprinting;
        Player.Shoot += Shoot;
        Player.ADS += AIM;
        StartCoroutine(LoadBandaid());
    }
    private void OnDestroy()
    {
        Player.Shoot -= Shoot;
        Player.ADS -= AIM;
    }
    private void OnEnable()
    {
        if(UpdatedLevel==false)
            StartCoroutine(LoadBandaid());

      
    }
    public void AIM(bool aiming)
    {
        if (gameObject.activeInHierarchy == true)
            _IsAiming = aiming;
    }
    public virtual void Update()
    {
        if ((Time.time < NextFire && gameObject.activeInHierarchy == true && info.IsPaused==false)||_IsSprinting==true)
            info.SetCanShoot(false);
        else if (Time.time > NextFire && gameObject.activeInHierarchy == true&&info.GetMag()> 0 && info.IsPaused == false)
            info.SetCanShoot(true);
    }
    public void DoDamage(HealthSystem Health, bool _IsCrit,Vector3 point, RaycastHit Hit)
    {
     //  Debug.Log("Damage done should be "+Damage*CalculateWeaponDamageFalloff(Hit.distance));
        float DamageX = 1.0f * CalculateWeaponDamageFalloff(Hit.distance);
     
        if (_IsCrit) DamageX = CritMultiplier;

        if (Health)
        {
            StartCoroutine(HitMarkerEffect(_IsCrit));
            Health.gameObject.AddComponent<DamageIndicator>().SetIndicator(transform,(int)(Damage*DamageX), _IsCrit);
            Health.gameObject.GetComponent<DamageIndicator>().SetHisPos(point);
            Health.ModifyHealth(transform,(int)(Damage * DamageX));
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
            return obj.transform.GetChild(0).GetComponent<HealthSystem>(); 
        else
            return FindBossHealth(obj.transform.parent.gameObject);     
    }
    public IEnumerator HitMarkerEffect(bool HitType)
    {
        int var = 0;
        if (HitType == true) var = 1;
        //0 = normal 1 = critical
        HitMarkers.transform.GetChild(var).gameObject.SetActive(true);
        yield return shotDuration;
        HitMarkers.transform.GetChild(var).gameObject.SetActive(false);
    }
    public IEnumerator ShotEffect()
    {
        FMODUnity.RuntimeManager.PlayOneShot(shootSound);
        MuzzleFlash.Play();
        LazerLine.enabled = true;
        LazerLine.SetPosition(0, GunEnd.position);
        yield return shotDuration;
        LazerLine.enabled = false;
    }
    private IEnumerator LoadBandaid()
    {
        yield return new WaitForSeconds(0.1f);
        LoadData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
    }
    public void LoadData(GameData data)
    {
        UpdatedLevel = true;
        PrimaryWeaponLvl = data.WeaponLevel;
        SecondaryWeaponLvl = data.SecondaryWeaponLevel;
        if (GetComponent<WeaponInfo>()._IsPrimaryWeapon==true)
        Level = data.WeaponLevel;
        else if (GetComponent<WeaponInfo>()._IsPrimaryWeapon==false)
        Level = data.SecondaryWeaponLevel;

        for (int i=0; i < Level; i++)
        {
            WeaponUpgrades(i+1);
        }
    }

    public void SaveData(GameData data)
    {
        if (GetComponent<WeaponInfo>()._IsPrimaryWeapon)
            data.WeaponLevel = Level;
        else if (!GetComponent<WeaponInfo>()._IsPrimaryWeapon)
            data.SecondaryWeaponLevel = Level;
    }
}
