using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannonAmmo : MonoBehaviour
{
    private WaitForSeconds BoomTimer = new WaitForSeconds(0.5f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
        collision.gameObject.GetComponentInParent<HealthSystem>().ModifyHealth(-100); 

        StartCoroutine(DestroyThis());
    }
    private IEnumerator DestroyThis()
    {
        gameObject.transform.localScale = new Vector3(10.0f, 6.0f, 10.0f);
        yield return BoomTimer;
        Destroy(gameObject);
    }
}
