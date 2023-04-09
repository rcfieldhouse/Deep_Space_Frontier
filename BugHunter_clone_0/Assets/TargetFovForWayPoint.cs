using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFovForWayPoint : MonoBehaviour
{
    public Camera CopyCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().fieldOfView = CopyCam.fieldOfView;
    }
}
