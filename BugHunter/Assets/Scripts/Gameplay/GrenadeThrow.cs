using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    private Rigidbody Rigidbody;
    private bool _isReady = true,_isExploding=false;
    private SphereCollider sphereCollider;
    private float GrenadeFuse = 2.0f;
    private WaitForSeconds BoomTimer = new WaitForSeconds(0.25f);
    private WaitForSeconds GrenadeResetTimer = new WaitForSeconds(5.0f);
    [SerializeField] int GrenadeDamage = 100;
    //Grenade VFX Variables
    public GameObject GrenadeVFX;
    //private GameObject EffectToSpawn;
    private MeshRenderer GrenadeRenderer;
    private Vector3 grenadeSpawnPos;

    // Start is called before the first frame update
    void Awake()
    {
        //_startValues.position = new Vector3(-0.5f, -0.03f, 0.4f);
       // _startValues.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null) Invoke(nameof(KillThis), 0.5f);
        else Rigidbody.isKinematic = true;


        sphereCollider = GetComponent<SphereCollider>();
       
        GrenadeRenderer = GetComponent<MeshRenderer>();
    }
    void KillThis()
    {
        Destroy(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Rigidbody.isKinematic = true;
            transform.parent = collision.transform;
        }
           
    }

    public bool GetIsReady()
    {
        return _isReady;
    }
    public void ThowGrenade(Vector3 ThrowVector)
    {
        
        if (_isReady == true){
            _isReady = false;
            Rigidbody.gameObject.SetActive(true);
            Rigidbody.isKinematic = false;
            Rigidbody.transform.parent = null;
            Rigidbody.velocity = ThrowVector;
            sphereCollider.radius = 0.02f;
            sphereCollider.enabled = true;
            sphereCollider.isTrigger = false;
            Invoke(nameof(TingGoBoom), GrenadeFuse);

        }
    }

    public void TingGoBoom()
    {
        transform.parent = null;
        GrenadeRenderer.enabled = false;
        Rigidbody.isKinematic = true;
        sphereCollider.enabled = true;
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0.77f;
        _isExploding = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Projectiles/Grenade_Explosion");
        //Grenade VFX Trigger
        SpawnGrenadeVFX();
        Invoke(nameof(KillIt), 0.1f);  
    }
    private void KillIt()
    {
        Destroy(gameObject);
    }
    public bool GetIsExploding()
    {
        return _isExploding;
    }
   
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<HealthSystem>()&&other.tag!="Player")
        other.gameObject.GetComponentInParent<HealthSystem>().ModifyHealth(transform,-GrenadeDamage);
        if (other.gameObject.GetComponent<HealthSystem>() && other.tag == "Player")
            other.gameObject.GetComponentInParent<HealthSystem>().ModifyHealth( -GrenadeDamage);


    }
    // function for spawning the grenade VFX on it's current position
    public void SpawnGrenadeVFX()
    {
        if (GrenadeVFX != null)
        {
            grenadeSpawnPos.x = gameObject.transform.position.x;
            grenadeSpawnPos.y = gameObject.transform.position.y + 3.15f;
            grenadeSpawnPos.z = gameObject.transform.position.z;

            Instantiate(GrenadeVFX, grenadeSpawnPos, Quaternion.identity);
            //Debug.Log("Grenade VFX Should play here");

        }
        else
        {
            Debug.Log("GrenadeVFX has no VFX Attached");
        }
    }


}
