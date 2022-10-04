using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannonAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyThis();
    }
    private void DestroyThis()
    {
        Destroy(this);
    }
}
