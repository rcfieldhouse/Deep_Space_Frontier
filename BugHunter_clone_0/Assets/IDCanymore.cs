using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCanymore : MonoBehaviour
{
    GameObject Target; 
    // Update is called once per frame
    void Update()
    {
        Transform Player = Target.transform.parent.GetChild(0);
        transform.position = Target.transform.position;
        Debug.Log("Rotation 1: " + Player.rotation);
        transform.rotation = Player.rotation;
        Debug.Log("Rotation 2: " + Player.rotation);
        Debug.Log("Target: " + Player.name);
    }
    public void SetTarget (GameObject obj)
    {
        Target = obj;

    }
}
