using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveRock : MonoBehaviour
{
    public Material[] materials;
    public MeshRenderer Mesh;
    public float dissolveRate = 0.005f;
    public float refreshRate = 0.02f;
    public float counter = 0;
    public float Delay=0;
    // Start is called before the first frame update
    void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();
        if (Mesh != null)
            materials = Mesh.materials;
    }

    // Update is called once per frame
    void Update()
    {
        Delay += Time.deltaTime;
        if(Delay<3.0f)
        return;

        if (materials.Length > 0)
        {
            
            if (materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += Time.deltaTime/2.5f;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
            }
            else
                Destroy(gameObject);
            //NetworkDestroy
        }
    }
  
}
