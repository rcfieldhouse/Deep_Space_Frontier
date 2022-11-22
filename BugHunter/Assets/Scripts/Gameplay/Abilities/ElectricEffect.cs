using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffect : MonoBehaviour
{
    bool _TargetFound = false;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.AddComponent<BeenElectrified>();
        StartCoroutine(ArcShot());
    }
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.tag == "Enemy" && other.GetComponent<ElectricEffect>()==null&&_TargetFound==false && other.GetComponent<BeenElectrified>() == null)
        {
            _TargetFound = true;
            StartCoroutine(ShowLine(other.transform));
            Debug.Log(gameObject.name+" transferring to "+ other.gameObject.name);
            other.gameObject.AddComponent<ElectricEffect>(); 

        }
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (other.tag == "Enemy" && other.GetComponent<ElectricEffect>() == null && _TargetFound == false && other.GetComponent<BeenElectrified>()==null)
        {
            _TargetFound = true;
            StartCoroutine(ShowLine(other.transform));
            Debug.Log(gameObject.name + " transferring to " + other.gameObject.name);
            other.gameObject.AddComponent<ElectricEffect>();
        }
    }

    // Update is called once per frame
    private IEnumerator ArcShot()
    {
        Rigidbody rigidbody;
        if (gameObject.GetComponent<Rigidbody>() == false)
        {
             rigidbody = gameObject.AddComponent<Rigidbody>();
        }else
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        SphereCollider coll= gameObject.AddComponent<SphereCollider>();
        coll.radius = 15.0f;
        coll.isTrigger = true;
     
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<HealthSystem>().ModifyHealth(-25);
        Destroy(rigidbody);
        Destroy(coll);
        Destroy(this);
    }
    private IEnumerator ShowLine(Transform Target)
    {
        Debug.Log("line ");
        LineRenderer Line = gameObject.AddComponent<LineRenderer>();
        Line.enabled = true;
        Line.startWidth = 0.5f;
        Line.endWidth = 0.1f;
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, Target.position);
        yield return new WaitForSeconds(0.5f);
        Destroy(Line);
    }
    private void OnDestroy()
    {
        if (GetComponent<LineRenderer>())
            Destroy(GetComponent<LineRenderer>());
    }
}
