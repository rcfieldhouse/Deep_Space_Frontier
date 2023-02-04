using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    private bool _IsShooting = false;
    // Start is called before the first frame update
    private void Awake()
    {
        HitMarkers = transform.parent.parent.parent.parent.GetComponentInChildren<GUIHolder>().HitMarkers;
        info = GetComponent<WeaponInfo>();
        LazerLine = GetComponent<LineRenderer>();
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        Camera = transform.parent.GetComponentInParent<Camera>();
        GunEnd = MuzzleFlash.transform;
        PlayerInput.Shoot += SetShootingTrue;
        PlayerInput.Shoot += Shoot;
        PlayerInput.ADS += AIM;
        PlayerInput.Chamber += SetShootingFalse;
    }
    private void OnDestroy()
    {
        PlayerInput.Shoot -= SetShootingTrue;
        PlayerInput.Shoot -= Shoot;
        PlayerInput.ADS -= AIM;
        PlayerInput.Chamber -= SetShootingFalse;
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
            //gun info
            info.SetCanShoot(false);
            info.SetBulletCount();
            NextFire = Time.time + FireRate;

            //Bullet raycast
            Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit Hit;

            StartCoroutine(ShotEffect());
            if (Physics.Raycast(RayOrigin, Camera.transform.forward, out Hit, WeaponRange))
            {
                //Damage
                LazerLine.SetPosition(1, Hit.point);
                HealthSystem Health = FindHealth(Hit.collider);
                DoDamage(Health, Hit.collider.isTrigger);

                if (Hit.rigidbody != null)
                    Hit.rigidbody.AddForce(-Hit.normal * HitForce);

            }
            else
                LazerLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange));
        }
    }
    void Update()
    {
        if (_IsShooting == true)
        {
            PlayerInput.Shoot.Invoke();
        }

        if (Time.time < NextFire && gameObject.activeInHierarchy == true)
            info.SetCanShoot(false);
        else if (Time.time > NextFire && gameObject.activeInHierarchy == true && info.GetMag() > 0)
            info.SetCanShoot(true);
    }
}
