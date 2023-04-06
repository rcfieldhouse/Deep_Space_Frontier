using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
    public override void WeaponUpgrades(int lvl)
    {
        switch (lvl) {
            case 1:
                GetComponent<WeaponInfo>().ReloadTimer =new WaitForSeconds(GetComponent<WeaponInfo>()._reloadTimer *= 0.7f);
                    break;
            case 2:
                WeaponRange *= 1.15f;
                GetComponent<WeaponInfo>().RecoilX *= 1.10f;
                GetComponent<WeaponInfo>().AimRecoilX *= 1.05f;
                    break;
            case 3:
                Damage =(int)(Damage* 1.20f);
                    break;
        }

    }
    public override void Shoot()
    {
        base.Shoot();
        if (info.GetCanShoot() == false || gameObject.activeInHierarchy == false || info._isReloading == true)
            return;
            //Random Hipfire spray
            Vector3 Spread;
            Spread.x = Random.Range(-ShotSpread.x, ShotSpread.x);
            Spread.y = Random.Range(-ShotSpread.y, ShotSpread.y);
            Spread.z = 0.0f;
            //for ads accuracy
            if (_IsAiming == true)
                Spread *= (1 - ADS_Accuracy);

            //gun info
            info.SetCanShoot(false);
            info.SetBulletCount();
            NextFire = Time.time + FireRate;

            //Bullet raycast
            Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit Hit;
         
            StartCoroutine(ShotEffect());
            if (Physics.Raycast(RayOrigin, Camera.transform.forward * WeaponRange + Spread, out Hit, 500))
            {
                //Damage
                LazerLine.SetPosition(1, Hit.point);
                HealthSystem Health = FindHealth(Hit.collider);
            DoDamage(Health, Hit.collider.isTrigger, Hit.point,Hit);
          
            if (Hit.rigidbody != null)
                    Hit.rigidbody.AddForce(-Hit.normal * HitForce);

            }
            else
                LazerLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange) + Spread);
        

        
    }
}
