using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public GameObject player;
    private Rigidbody Rigidbody;
    private bool _isReady = true;
    private CapsuleCollider CapsuleCollider;
    private WaitForSeconds GrenadeFuse = new WaitForSeconds(2.0f);
    private WaitForSeconds BoomTimer = new WaitForSeconds(0.25f);
    private WaitForSeconds GrenadeResetTimer = new WaitForSeconds(5.0f);
    [SerializeField] int GrenadeDamage = 100;
     private Transform _startValues;
    // Start is called before the first frame update
    void Start()
    {
        //_startValues.position = new Vector3(-0.5f, -0.03f, 0.4f);
       // _startValues.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        Rigidbody.isKinematic = true;
        Rigidbody.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(ThowGrenade(Vector3.forward*5));

    }
    public Transform GetStartPos()
    {
        return _startValues;
    }
    public IEnumerator ThowGrenade(Vector3 ThrowVector)
    {
        if (_isReady == true) {
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
        Rigidbody.gameObject.transform.localScale *= 55.0f;
        Rigidbody.isKinematic = true;
        CapsuleCollider.isTrigger = true;
      //  Debug.Log("grenade1");
        yield return BoomTimer;
      //  Debug.Log("grenade2");
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
        CapsuleCollider.isTrigger = false;
        Rigidbody.gameObject.GetComponent<MeshRenderer>().enabled = false;

      //  Debug.Log("grenadeReset1");
        
        yield return GrenadeResetTimer;
       // Debug.Log("grenadeReset2");
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


}
