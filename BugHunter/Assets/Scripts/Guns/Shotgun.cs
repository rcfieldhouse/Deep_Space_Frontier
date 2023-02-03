using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private Vector3 ShotgunSpread;
    public override void Shoot()
    {
        if (info.GetCanShoot() == false)
            return;
        for (int i = 2; i <= 15; i += 2)
        {
            LazerLine.SetPosition(i, GunEnd.position);
        }
        info.SetCanShoot(false);
        info.SetBulletCount();
        NextFire = Time.time + FireRate;

        for (int i = 1; i <= 15; i += 2)
        {
            ShotgunSpread.x = Random.Range(-Hipfire_Spread.x, Hipfire_Spread.x);
            ShotgunSpread.y = Random.Range(-Hipfire_Spread.y, Hipfire_Spread.y);
            ShotgunSpread.z = 0.0f;
            ShotgunShot(ShotgunSpread,i);
        }
    }
    private void ShotgunShot(Vector3 Spread,int index)
    {
   
        //Bullet raycast
        Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit Hit;

        StartCoroutine(ShotEffect());
        if (Physics.Raycast(RayOrigin, Camera.transform.forward*WeaponRange+Spread, out Hit, WeaponRange))
        {
            //Damage
            LazerLine.SetPosition(index, Hit.point);
            HealthSystem Health = FindHealth(Hit.collider);
            DoDamage(Health, Hit.collider.isTrigger);

            if (Hit.rigidbody != null)
                Hit.rigidbody.AddForce(-Hit.normal * HitForce);

        }
        else
            LazerLine.SetPosition(index, RayOrigin + (Camera.transform.forward * WeaponRange)+Spread);
    }
}
