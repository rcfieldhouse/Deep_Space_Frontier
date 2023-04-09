using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKRightArm : MonoBehaviour
{
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponentInChildren<IKRightID>() == null)
            return;
        transform.position = transform.parent.GetComponentInChildren<IKRightID>().transform.position;
        transform.rotation = transform.parent.GetComponentInChildren<IKRightID>().transform.rotation;
        transform.localScale = transform.parent.GetComponentInChildren<IKRightID>().transform.localScale;
    }
}
