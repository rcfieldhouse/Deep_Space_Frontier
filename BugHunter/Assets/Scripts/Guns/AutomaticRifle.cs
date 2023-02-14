using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeParameters : ScriptableObject
{
    //damage
    //reload speed
    //magazine size


}

public class AutomaticRifle : Gun
{
    private bool _IsShooting = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Player = transform.parent.parent.parent.parent.GetChild(0).GetComponent<PlayerInput>();
        HitMarkers = transform.parent.parent.parent.parent.GetComponentInChildren<GUIHolder>().HitMarkers;
        info = GetComponent<WeaponInfo>();
        LazerLine = GetComponent<LineRenderer>();
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        Camera = transform.parent.GetComponentInParent<Camera>();
        GunEnd = MuzzleFlash.transform;
        Player.Shoot += SetShootingTrue;
        Player.Shoot += Shoot;
        Player.ADS += AIM;
        Player.Chamber += SetShootingFalse;
    }
    private void OnDestroy()
    {
        Player.Shoot -= SetShootingTrue;
        Player.Shoot -= Shoot;
        Player.ADS -= AIM;
        Player.Chamber -= SetShootingFalse;
    }
    private void SetShootingTrue()
    {
       _IsShooting = true;
    }
    private void SetShootingFalse()
    {
        _IsShooting = false;
    }
    public override void Shoot()
    {
        if (info.GetCanShoot() == true && gameObject.activeInHierarchy == true)
        {
            //For hipfire spray
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
   public override void Update()
    {
        

        if (_IsShooting == true)
            Player.AutomaticBandAid();
        base.Update();
    }
}
