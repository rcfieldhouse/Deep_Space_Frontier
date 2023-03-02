using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private Vector3 ShotgunSpread;
    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }

        while(S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
    public override void Shoot()
    {
        if (info.GetCanShoot() == false|| gameObject.activeInHierarchy == false)
            return;
        for (int i = 2; i <= 23; i += 2)
        {
            LazerLine.SetPosition(i, GunEnd.position);
        }
        info.SetCanShoot(false);
        info.SetBulletCount();
        NextFire = Time.time + FireRate;
       
        for (int i = 1; i <= 23; i += 2)
        {
            ShotgunSpread.x = RandomGaussian(-ShotSpread.x, ShotSpread.x);
            ShotgunSpread.y = RandomGaussian(-ShotSpread.y, ShotSpread.y);
            ShotgunSpread.z = 0.0f;

            if (_IsAiming == true)
                ShotgunSpread *= (1-ADS_Accuracy);

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
