using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CryogenicEffect : MonoBehaviour
{
    // How long enemies are frozen for
    private WaitForSeconds FreezeTime = new WaitForSeconds(3.0f);
    private int Damage;
    //private Vector4 FrozenColor = new Vector4(0f,0.5f,1f,1f);
    
    public void SetValues(Vector3 vec)
    {
        Damage = (int)vec.x;
        FreezeTime = new WaitForSeconds(vec.y);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Invoke(nameof(Effect), 0.05f);
    }
    private void Effect()
    {
        // how much damage a shot from cryo bullet will do
        gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
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
