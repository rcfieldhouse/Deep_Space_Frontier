using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Gun
{
    public override void WeaponUpgrades(int lvl)
    {
        switch (lvl)
        {
            case 1:
                GetComponent<WeaponInfo>().magSize += 3;
                break;
            case 2:
                GetComponent<WeaponInfo>().ReloadTimer = new WaitForSeconds(GetComponent<WeaponInfo>()._reloadTimer *= 0.9f);
                break;
            case 3:
                GetComponent<WeaponInfo>().RecoilX *= 0.9f;
                GetComponent<WeaponInfo>().AimRecoilX *= 0.9f;
                break;
        }
    }
    private SpecialBulletSelect CurrentBullet;
    private BulletInfo BulletInfo;
    [Range(0, 20)] public int[] SpecialBulletCapacity;
    private Vector3[] SpecialBulletInfo;
    private void OnEnable()
    {
        BulletInfo = GetComponent<BulletInfo>();
        CurrentBullet = transform.parent.parent.parent.parent.GetComponentInChildren<SpecialBulletSelect>();
    }
   
    // Start is called before the first frame update
    public override void Shoot()
    {
        base.Shoot();
        if (info.GetCanShoot() == false || gameObject.activeInHierarchy == false || info._isReloading == true)
            return;
            //gun info
            info.SetCanShoot(false);
            info.SetBulletCount();

            if((int)CurrentBullet.GetBulletType() != 0)
            SpecialBulletCapacity[(int)CurrentBullet.GetBulletType()]--;

           // Debug.Log(SpecialBulletCapacity[(int)CurrentBullet.GetBulletType()]);
            NextFire = Time.time + FireRate;

            //Bullet raycast
            Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit Hit;


             GameObject Shell = Instantiate(AmmoCasingPrefab);
             Shell.transform.position = CasingEjectPoint.position;
             Shell.GetComponent<Rigidbody>().AddForce(CasingEjectPoint.transform.rotation * Vector3.right * 10.0f, ForceMode.Impulse);

        StartCoroutine(ShotEffect());
            if (Physics.Raycast(RayOrigin, Camera.transform.forward, out Hit, WeaponRange))
            {
                //Damage
                Debug.Log(Hit.collider.name);
                LazerLine.SetPosition(1, Hit.point);
                HealthSystem Health = FindHealth(Hit.collider);
            DoDamage(Health, Hit.collider.isTrigger, Hit.point);
            if (Hit.rigidbody != null)
                    Hit.rigidbody.AddForce(-Hit.normal * HitForce);
                if(Health)
                CurrentBullet.CallShotEffect(Health.gameObject,BulletInfo.GetData(), Hit.collider.isTrigger);
              
            }
            else
                LazerLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange));

        
    }
    public override void Update()
    {
        base.Update();

         if (SpecialBulletCapacity[(int)CurrentBullet.GetBulletType()] <= 0 && (int)CurrentBullet.GetBulletType()!=0)
             info.SetCanShoot(false); 
     }
}
