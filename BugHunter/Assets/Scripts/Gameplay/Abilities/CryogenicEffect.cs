using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CryogenicEffect : MonoBehaviour
{
    // How long enemies are frozen for
    private WaitForSeconds FreezeTime = new WaitForSeconds(3.0f);
    //private Vector4 FrozenColor = new Vector4(0f,0.5f,1f,1f);
    


    // Start is called before the first frame update
    private void Awake()
    {
        // how much damage a shot from cryo bullet will do
        gameObject.GetComponent<HealthSystem>().ModifyHealth(-50);
        //called when attached to de enemy 
        StartCoroutine(FreezeDeBoi());
    }
    private IEnumerator FreezeDeBoi()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;


        GetComponent<NavMeshAgent>().enabled=false;
        yield return FreezeTime;
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;

        GetComponent<NavMeshAgent>().enabled =true;

        Destroy(this);
    }
}
