using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CryogenicEffect : MonoBehaviour
{
    private WaitForSeconds FreezeTime = new WaitForSeconds(3.0f);
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //called when attached to de enemy 
        StartCoroutine(FreezeDeBoi());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FreezeDeBoi()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled=false;
        yield return FreezeTime;
        gameObject.GetComponent<NavMeshAgent>().enabled =true;
        Destroy(this);
    }
}
