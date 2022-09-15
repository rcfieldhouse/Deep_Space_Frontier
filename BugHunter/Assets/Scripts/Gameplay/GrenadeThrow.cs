using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public Rigidbody rigidbody;
    private WaitForSeconds GrenadeFuse = new WaitForSeconds(2.0f);
    private WaitForSeconds BoomTimer = new WaitForSeconds(0.25f);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        rigidbody.gameObject.transform.localScale *= 25.0f;

        yield return BoomTimer;

        rigidbody.gameObject.SetActive(false);
    }

}
