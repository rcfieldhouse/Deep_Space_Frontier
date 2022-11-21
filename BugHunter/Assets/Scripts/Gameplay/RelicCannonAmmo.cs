using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannonAmmo : MonoBehaviour
{
    private WaitForSeconds Delay = new WaitForSeconds(0.5f);
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(KillShot());
        StartCoroutine(Enlarge());
    }
    private void OnCollisionEnter(Collision collision)
    { 
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(ShowLine(other.gameObject.transform));
            other.gameObject.GetComponent<HealthSystem>().ModifyHealth(-100);
        }
    }
    private IEnumerator KillShot()
    {
        yield return new WaitForSeconds(15.0f);
        Destroy(gameObject);
    }
    private IEnumerator Enlarge()
    {
        yield return Delay;
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
        gameObject.GetComponent<SphereCollider>().radius = 8.0f;
    }


    private IEnumerator ShowLine(Transform Target)
    {
        LineRenderer Line = gameObject.AddComponent<LineRenderer>();
        Line.startWidth = 0.1f;
        Line.endWidth = 0.1f;
        Line.enabled = true;
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, Target.position);
        yield return new WaitForSeconds(0.075f);
        Destroy(Line);
    }
}
