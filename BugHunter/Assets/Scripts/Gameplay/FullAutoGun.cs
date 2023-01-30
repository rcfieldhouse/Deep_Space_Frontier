using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAutoGun : MonoBehaviour
{
    private bool _IsShooting = false; 
    public int gunDamage = -25;                                            // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                        // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                        // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;
    public WeaponInfo info;                                                    // Holds a reference to the gun end object, marking the muzzle location of the gun
    [SerializeField]
    public Camera fpsCam;                                                // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    [Range(0, 3)] public float CritMultiplier = 1.0f;
    private LineRenderer laserLine;
    // Reference to the LineRenderer component which will display our laserline
    private ParticleSystem muzzleFlash;
    private float nextFire;
    private GameObject HitMarkers;
    // Start is called before the first frame update
    void Awake()
    {
        info = GetComponent<WeaponInfo>();
        laserLine = GetComponent<LineRenderer>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();

        // Get and store a reference to our AudioSource component

        HitMarkers = transform.parent.parent.parent.parent.GetComponentInChildren<GUIHolder>().HitMarkers;

        //observers
        PlayerInput.Shoot += Shoot;
        PlayerInput.Shoot += SetShootingTrue;
        PlayerInput.Chamber += SetShootingFalse;
    }
    private void OnDestroy()
    {
        PlayerInput.Shoot -= Shoot;
        PlayerInput.Shoot -= SetShootingTrue;
        PlayerInput.Chamber -= SetShootingFalse;
    }
    private void SetShootingTrue()
    {
        if (gameObject.GetComponent<WeaponInfo>().GetCanShoot() == true) _IsShooting = true;
        // 2 functions so i dont need to pass data to all functs

    }
    private void SetShootingFalse()
    {
        _IsShooting = false;
    }
    private HealthSystem FindBossHealth(GameObject obj)
    {
       // Debug.Log(obj.name);
        if (obj.tag == "Boss")
        {
            Debug.Log("we found the boss "+ obj.name);
            return obj.GetComponent<HealthSystem>();
        }
        else
        {
            return FindBossHealth(obj.transform.parent.gameObject);
           
        }
        //error prevention
  
    }
    private void Shoot()
    {
        if (Time.time > nextFire && info.GetMag() > 0 && gameObject.activeInHierarchy==true&& info.GetCanShoot() == true)
        {
            //play Dante.sound.ogg full auto effect
            info.SetBulletCount();
            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;

            // Start our ShotEffect coroutine to turn our laser line on and off


            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, gunEnd.position);



            StartCoroutine(ShotEffect());
            // Check if our raycast has hit anything
            //for basic guns

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                // Set the end position for our laser line 
                laserLine.SetPosition(1, hit.point);

                HealthSystem health;
                // Get a reference to a health script attached to the collider we hit
                if (hit.collider.tag == "BossColliderHolder")
                {
                    health = FindBossHealth(hit.collider.gameObject);
            
                }
                else {
                    health = hit.collider.GetComponent<HealthSystem>();
                }

               

                // If there was a health script attached
                if (health != null && hit.collider.isTrigger)
                {                 
                    StartCoroutine(HitMarkerEffect(1));
                    // Double Damage for Crits
                    health.ModifyHealth((int)(gunDamage * CritMultiplier));
                }
                else if (health != null)
                {
            
                    StartCoroutine(HitMarkerEffect(0));
                    // Call the damage function of that script, passing in our gunDamage variable
                    health.ModifyHealth(gunDamage);
                }
                    



                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));

            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_IsShooting == true)
        {
            if (gameObject.GetComponent<WeaponInfo>().GetCanShoot() == false) _IsShooting = false;
            PlayerInput.Shoot.Invoke(); ;          
        }
          
    }
    private IEnumerator HitMarkerEffect(int HitType)
    {
        //Hit type 0 is normal Hit Type 1 is Crit
        HitMarkers.transform.GetChild(HitType).gameObject.SetActive(true);
        yield return shotDuration;
        HitMarkers.transform.GetChild(HitType).gameObject.SetActive(false);
    }
    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        //SoundManager.instance.PlaySound(gunAudio);

        //play Shooting Effect
        muzzleFlash.Play();

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
}
