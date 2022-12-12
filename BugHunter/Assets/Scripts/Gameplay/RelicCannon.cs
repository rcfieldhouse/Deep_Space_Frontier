using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannon : MonoBehaviour
{
    [Range(0, -500)] public int Damage=0;
    public GameObject bulletOfDoom;
    public Transform bulletEmitter;
    [SerializeField] float shotStrength=20;
    private WaitForSeconds ShotDelay = new WaitForSeconds(5.0f);
    [SerializeField] private bool isReady = true;
    public WeaponInfo info;
    // Start is called before the first frame update
    void Awake()
    {
        info = GetComponentInParent<WeaponInfo>();
        PlayerInput.Shoot += Shoot;
    }
    private void OnDestroy()
    {
        PlayerInput.Shoot -= Shoot;
    }
    // Update is called once per frame

    private void Shoot()
    {
        //this exists cause we cant pass an enum to a action of return type void
        if(gameObject.activeInHierarchy == true)
            StartCoroutine(shoot());
    }
    private IEnumerator shoot()
    {
        if (isReady == true && info.GetMag() > 0 )
        {
            isReady = false;
            info.SetBulletCount();       
            Rigidbody rigidbody = Instantiate(bulletOfDoom, bulletEmitter.position, Quaternion.identity).GetComponent<Rigidbody>();
            rigidbody.gameObject.GetComponent<RelicCannonAmmo>().SetDamage(Damage);
            rigidbody.velocity=(transform.forward * shotStrength);
        }
       
        
        yield return ShotDelay;    
       isReady = true;
       

    }
}
