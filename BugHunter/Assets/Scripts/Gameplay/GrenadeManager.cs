using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    private Transform Transform;
    public GrenadeThrow GrenadeThrow; 
    private int numGrenades=3;
    // Start is called before the first frame update
    void Start()
    {
        GrenadeThrow = GetComponent<GrenadeThrow>();
        Transform = GrenadeThrow.GetStartPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw()
    {

    }
    public void ResetPos()
    {
   
    }
}
