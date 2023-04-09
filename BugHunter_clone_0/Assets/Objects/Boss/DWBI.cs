using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWBI : MonoBehaviour
{
    public Vector3 vector3;
    Quaternion quat;
    private void Awake()
    {
         quat = transform.rotation;
    }
    private void LateUpdate()
    {
      
        transform.rotation = quat;
    }
}
