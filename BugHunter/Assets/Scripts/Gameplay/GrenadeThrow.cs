using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CapsuleCollider CapsuleCollider;
    private WaitForSeconds GrenadeFuse = new WaitForSeconds(2.0f);
    private WaitForSeconds BoomTimer = new WaitForSeconds(0.25f);
    [SerializeField] int GrenadeDamage = 100;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(ThowGrenade(Vector3.forward*5));

    }
    public IEnumerator ThowGrenade(Vector3 ThrowVector)
    {
        Debug.Log("GrenadeThrow");
        rigidbody.isKinematic = false;
        rigidbody.transform.parent = null;
        rigidbody.velocity=ThrowVector;

        yield return GrenadeFuse;

        StartCoroutine(TingGoBoom());
        
    }

    public IEnumerator TingGoBoom()
    {
        rigidbody.gameObject.transform.localScale *= 55.0f;
        rigidbody.isKinematic = true;
        CapsuleCollider.isTrigger = true;

        yield return BoomTimer;

        rigidbody.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<HealthSystem>())
        other.gameObject.GetComponentInParent<HealthSystem>().ModifyHealth(-GrenadeDamage); ;
       
    }
}
