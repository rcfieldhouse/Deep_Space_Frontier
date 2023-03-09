using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffect : MonoBehaviour
{
    private int Damage;
    private float radius,BT;
    private WaitForSeconds BoltTimer;
    bool _TargetFound = false;
    private Transform PlayerTransform;
    // Start is called before the first frame update
    void Awake()
    {
       Invoke(nameof(Effect), 0.05f);
    }
    private void Effect()
    {
        gameObject.AddComponent<BeenElectrified>();
        StartCoroutine(ArcShot());
    }
    public void SetValues(Vector3 vec)
    {
        Damage = (int)vec.x;
        radius = vec.y;
        BoltTimer = new WaitForSeconds(vec.z);
        BT = vec.z;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Enemy" && other.GetComponent<ElectricEffect>()==null&&_TargetFound==false && other.GetComponent<BeenElectrified>() == null)
        {
            _TargetFound = true;
            StartCoroutine(ShowLine(other.transform));         
            StartCoroutine(NewTarget(other));
            Debug.Log(other + " has been electrified");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Enemy" && other.GetComponent<ElectricEffect>() == null && _TargetFound == false && other.GetComponent<BeenElectrified>()==null)
        {
            _TargetFound = true;
            StartCoroutine(ShowLine(other.transform));
            StartCoroutine(NewTarget(other));
            Debug.Log(other+" has been electrified");
        }
    }
    public void SetRecFrom(Transform transform)
    {
        PlayerTransform = transform;
    }
    private IEnumerator NewTarget(Collider collider)
    {
        // how long to move to each enemy
        yield return BoltTimer;
        collider.gameObject.AddComponent<ElectricEffect>().SetValues(new Vector3(Damage,radius,BT));
        collider.gameObject.GetComponent<ElectricEffect>().SetRecFrom(GetComponent<DamageIndicator>().DamageReceivedFrom);
    }
    // Update is called once per frame
    private IEnumerator ArcShot()
    {
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
        Rigidbody rigidbody;
        if (gameObject.GetComponent<Rigidbody>() == false)
        {
             rigidbody = gameObject.AddComponent<Rigidbody>();
        }else
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }
        rigidbody.isKinematic = true;
        SphereCollider coll= gameObject.AddComponent<SphereCollider>();
        // enemy arc to radius
        coll.radius = radius;
        coll.isTrigger = true;
        if (GetComponent<DamageIndicator>())
        {
            Transform transform = GetComponent<DamageIndicator>().DamageReceivedFrom;
            gameObject.AddComponent<DamageIndicator>().SetIndicator(transform, Damage, false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Sniper_Electric");
        }
        else gameObject.AddComponent<DamageIndicator>().SetIndicator(PlayerTransform, (int)(Damage), false);

        yield return new WaitForSeconds(1f);
        // how much damage electric bullet does
    

        gameObject.GetComponent<HealthSystem>().ModifyHealth(transform,Damage);
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        Destroy(rigidbody);
        Destroy(coll);
        Destroy(this);
    }
    private IEnumerator ShowLine(Transform Target)
    {
       
        LineRenderer Line = gameObject.AddComponent<LineRenderer>();
        Line.enabled = true;
        Line.startWidth = 0.5f;
        Line.endWidth = 0.1f;
        Line.startColor = Color.blue;
        Line.endColor = Color.blue;
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, Target.position);
        yield return new WaitForSeconds(0.75f);
        Destroy(Line);
    }
    private void OnDestroy()
    {
        if (GetComponent<LineRenderer>())
            Destroy(GetComponent<LineRenderer>());
    }
}
