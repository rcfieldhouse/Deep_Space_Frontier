using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannon : MonoBehaviour
{
    [Range(0, -500)] public int Damage=0;
    public GameObject bulletOfDoom;
    public Transform bulletEmitter;
    [SerializeField] float shotStrength=20;
    public WeaponInfo info;
    public PlayerInput Player;
    // Start is called before the first frame update
    void Awake()
    {      
        info = GetComponent<WeaponInfo>();
        Invoke(nameof(Wait), 0.1f);
    }
    void Wait()
    {
        Player = transform.parent.parent.parent.parent.GetChild(0).GetComponent<PlayerInput>();
        Player.Shoot += Shoot;
    }
    private void OnDestroy()
    {
        Player.Shoot -= Shoot;
    }
    // Update is called once per frame

    private void Shoot()
    {
        //this exists cause we cant pass an enum to a action of return type void
        if(gameObject.activeInHierarchy == true && info.GetMag() > 0)
        {
            info.SetBulletCount();
            Rigidbody rigidbody = Instantiate(bulletOfDoom, bulletEmitter.position, Quaternion.identity).GetComponent<Rigidbody>();
            rigidbody.gameObject.GetComponent<RelicCannonAmmo>().SetDamage(Damage);
            rigidbody.AddForce(transform.rotation*Vector3.forward * shotStrength,ForceMode.Impulse);
            rigidbody.transform.parent = null;
        }
    }
}
