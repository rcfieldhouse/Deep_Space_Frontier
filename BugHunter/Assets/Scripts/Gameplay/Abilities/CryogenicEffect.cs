using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CryogenicEffect : MonoBehaviour
{
    private WaitForSeconds FreezeTime = new WaitForSeconds(3.0f);
    private Material BaseMat;
    //private Vector4 FrozenColor = new Vector4(0f,0.5f,1f,1f);
    private Material FrozenMat;
    


    // Start is called before the first frame update
    private void Awake()
    {
        FrozenMat = Resources.Load("Frozen", typeof(Material)) as Material;
        BaseMat = GetComponentInChildren<SkinnedMeshRenderer>().material;


        //called when attached to de enemy 
        StartCoroutine(FreezeDeBoi());
    }
    private IEnumerator FreezeDeBoi()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = FrozenMat;
        Debug.Log("DOOT");

        GetComponent<NavMeshAgent>().enabled=false;
        yield return FreezeTime;
        GetComponentInChildren<SkinnedMeshRenderer>().material = BaseMat;

        //BaseMat.SetVector("DamageColor", new Vector4(1, 1, 1, 1));

        GetComponent<NavMeshAgent>().enabled =true;

        Destroy(this);
    }
}
