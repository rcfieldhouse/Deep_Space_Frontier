using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCanymore : MonoBehaviour
{
    GameObject Target; 
    // Update is called once per frame
    void Update()
    {
        transform.position = Target.transform.position;
        transform.rotation = Target.transform.parent.GetChild(0).transform.rotation;
    }
    public void SetTarget (GameObject obj)
    {
        Target = obj;
    }
}
