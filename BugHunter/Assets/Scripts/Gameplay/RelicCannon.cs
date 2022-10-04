using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannon : MonoBehaviour
{
    public GameObject bulletOfDoom;
    public Transform bulletEmitter;
    [SerializeField] float shotStrength=20;
    private WaitForSeconds ShotDelay = new WaitForSeconds(5.0f);
    [SerializeField] private bool isReady = true;
    public Mag Magazine;
    // Start is called before the first frame update
    void Start()
    {
        Magazine = GetComponentInParent<Mag>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            
                StartCoroutine(shoot());
          
        }
    }
    private IEnumerator shoot()
    {
        if (isReady == true && Magazine.GetMag() > 0)
        {
            isReady = false;
            Magazine.SetBulletCount();       
            Rigidbody rigidbody = Instantiate(bulletOfDoom, bulletEmitter.position, Quaternion.identity).GetComponent<Rigidbody>();
            rigidbody.velocity=(transform.forward * shotStrength);
        }
       
        
        yield return ShotDelay;    
       isReady = true;
       

    }
}
