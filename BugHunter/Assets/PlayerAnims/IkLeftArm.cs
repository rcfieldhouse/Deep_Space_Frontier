using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkLeftArm : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.GetComponentInChildren<IKLeftID>().transform.position;
        transform.rotation = transform.parent.GetComponentInChildren<IKLeftID>().transform.rotation;
        transform.localScale = transform.parent.GetComponentInChildren<IKLeftID>().transform.localScale;
    }
}
