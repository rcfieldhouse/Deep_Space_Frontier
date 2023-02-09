using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Gun
{
    public SpecialBulletSelect CurrentBullet;
    private void OnEnable()
    {
        CurrentBullet = transform.parent.parent.parent.parent.GetComponentInChildren<SpecialBulletSelect>();
    }
    // Start is called before the first frame update
    public override void Shoot()
    {

        if (info.GetCanShoot() == true)
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
                Debug.Log(Hit.collider.name);
                LazerLine.SetPosition(1, Hit.point);
                HealthSystem Health = FindHealth(Hit.collider);
                DoDamage(Health, Hit.collider.isTrigger);

                if (Hit.rigidbody != null)
                    Hit.rigidbody.AddForce(-Hit.normal * HitForce);

                CurrentBullet.CallShotEffect(Health.gameObject);
            }
            else
                LazerLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange));

        }
    }
}
