using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CryogenicEffect : MonoBehaviour
{
    // How long enemies are frozen for
    private WaitForSeconds FreezeTime = new WaitForSeconds(3.0f);
    private Material BaseMat;
    private Vector4 FrozenColor = new Vector4(0f,0.5f,1f,1f);
    private Material FrozenMat = Resources.Load("Frozen", typeof(Material)) as Material;
    


    // Start is called before the first frame update
    private void Awake()
    {
        BaseMat = GetComponentInChildren<MeshRenderer>().material;
        // how much damage a shot from cryo bullet will do
        gameObject.GetComponent<HealthSystem>().ModifyHealth(-50);
        //called when attached to de enemy 
        StartCoroutine(FreezeDeBoi());
    }
    private IEnumerator FreezeDeBoi()
    {
        GetComponentInChildren<MeshRenderer>().material = FrozenMat;


        GetComponent<NavMeshAgent>().enabled=false;
        yield return FreezeTime;
        GetComponentInChildren<MeshRenderer>().material = BaseMat;

        //BaseMat.SetVector("DamageColor", new Vector4(1, 1, 1, 1));

        GetComponent<NavMeshAgent>().enabled =true;

        Destroy(this);
    }
}
