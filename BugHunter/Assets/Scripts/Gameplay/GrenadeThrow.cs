using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public GameObject player;
    private Rigidbody Rigidbody;
    private bool _isReady = true;
    private CapsuleCollider CapsuleCollider;
    private SphereCollider sphereCollider;
    private WaitForSeconds GrenadeFuse = new WaitForSeconds(2.0f);
    private WaitForSeconds BoomTimer = new WaitForSeconds(0.25f);
    private WaitForSeconds GrenadeResetTimer = new WaitForSeconds(5.0f);
    [SerializeField] int GrenadeDamage = 100;
     private Transform _startValues;
    //Grenade VFX Variables
    public GameObject GrenadeVFX;
    //private GameObject EffectToSpawn;
    private MeshRenderer GrenadeRenderer;
    private Vector3 grenadeSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        //_startValues.position = new Vector3(-0.5f, -0.03f, 0.4f);
       // _startValues.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        Rigidbody.isKinematic = true;
        GrenadeRenderer = GetComponent<MeshRenderer>();
    }


    public Transform GetStartPos()
    {
        return _startValues;
    }
    public bool GetIsReady()
    {
        return _isReady;
    }
    public IEnumerator ThowGrenade(Vector3 ThrowVector)
    {
        
        if (_isReady == true){
            _isReady = false;
        Rigidbody.gameObject.SetActive(true);
        Rigidbody.isKinematic = false;
        Rigidbody.transform.parent = null;
        Rigidbody.velocity=ThrowVector;
        
            yield return GrenadeFuse;
          
            StartCoroutine(TingGoBoom());
        }
    }

    public IEnumerator TingGoBoom()
    {
        GrenadeRenderer.enabled = false;
        Rigidbody.isKinematic = true;
        sphereCollider.enabled = true;
        
        //Grenade VFX Trigger
        SpawnGrenadeVFX();
        yield return BoomTimer;
        _isReady = false;


        StartCoroutine(ResetNade());
       
    }
    public IEnumerator ResetNade()
    {
       

        //dunno why but transform isnt writable
        Rigidbody.gameObject.transform.SetParent(player.transform);
        Rigidbody.gameObject.transform.localPosition = new Vector3(-0.5f, 1.03f, 0.4f);
        Rigidbody.gameObject.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        Rigidbody.gameObject.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        Rigidbody.isKinematic = true;
        sphereCollider.enabled = false;
        Rigidbody.gameObject.GetComponent<MeshRenderer>().enabled = false;

        
        yield return GrenadeResetTimer;
        _isReady = true;
        Rigidbody.gameObject.transform.localPosition = new Vector3(-0.5f, -0.03f, 0.4f);
        Rigidbody.gameObject.GetComponent<MeshRenderer>().enabled = true;
        Rigidbody.gameObject.SetActive(false);
     
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<HealthSystem>())
        other.gameObject.GetComponentInParent<HealthSystem>().ModifyHealth(-GrenadeDamage); ;
       
    }
    // function for spawning the greande VFX on it's current position
    public void SpawnGrenadeVFX()
    {
        if (GrenadeVFX != null)
        {
            grenadeSpawnPos.x = gameObject.transform.position.x;
            grenadeSpawnPos.y = gameObject.transform.position.y + 1.15f;
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
