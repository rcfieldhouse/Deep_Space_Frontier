using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Gun
{
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
