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
    private AI ai;
    private Material[] cachedMaterials;
    private Renderer Renderer;

    private void Start()
    {
        ai = GetComponentInParent<AI>();
        Renderer = GetComponentInChildren<Renderer>();

        cachedMaterials = Renderer.materials;
        
    }

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
        if (GetComponent<DamageIndicator>())
        {
            Transform transform = GetComponent<DamageIndicator>().DamageReceivedFrom;
            gameObject.AddComponent<DamageIndicator>().SetIndicator(transform, Damage,false);
        }
        // how much damage a shot from cryo bullet will do
        gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
        //called when attached to de enemy 
        StartCoroutine(FreezeDeBoi());
    }
    private IEnumerator FreezeDeBoi()
    {
        Debug.Log("DOOT");

        var mats = new Material[Renderer.materials.Length];
        for (var j = 0; j < Renderer.materials.Length; j++)
        {
            mats[j] = ai.iceMaterial;
        }
        Renderer.materials = mats;

        GetComponentInParent<NavMeshAgent>().enabled=false;
        GetComponent<Animator>().enabled = false;
        yield return FreezeTime;
        Renderer.materials = cachedMaterials;
        GetComponentInParent<NavMeshAgent>().enabled =true;
        GetComponent<Animator>().enabled = true;

        Destroy(this);
    }
}
