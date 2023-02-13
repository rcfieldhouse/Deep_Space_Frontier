using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWBI : MonoBehaviour
{
    public Quaternion Quaternion;
    private void Awake()
    {
        transform.rotation = Quaternion;
    }
}
