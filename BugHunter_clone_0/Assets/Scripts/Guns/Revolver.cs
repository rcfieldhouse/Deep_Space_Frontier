using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
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
            if (Physics.Raycast(RayOrigin, Camera.transform.forward * WeaponRange + Spread, out Hit, WeaponRange))
            {
                //Damage
                LazerLine.SetPosition(1, Hit.point);
                HealthSystem Health = FindHealth(Hit.collider);
                DoDamage(Health, Hit.collider.isTrigger);

                if (Hit.rigidbody != null)
                    Hit.rigidbody.AddForce(-Hit.normal * HitForce);

            }
            else
                LazerLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange) + Spread);
        

        
    }
}
