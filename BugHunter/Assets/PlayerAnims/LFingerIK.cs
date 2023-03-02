using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFingerIK : MonoBehaviour
{
 
    // Update is called once per frame
    void Update()
    {

        if (transform.parent.parent.GetComponentInChildren<RThumbID>() == null)
            return;

        transform.position = transform.parent.parent.GetComponentInChildren<LFingerID>().transform.position;
        transform.rotation = transform.parent.parent.GetComponentInChildren<LFingerID>().transform.rotation;
        transform.localScale = transform.parent.parent.GetComponentInChildren<LFingerID>().transform.localScale;
    }
}
