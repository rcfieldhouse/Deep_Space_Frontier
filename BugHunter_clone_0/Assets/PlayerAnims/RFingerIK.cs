using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFingerIK : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        if (transform.parent.parent.GetComponentInChildren<RThumbID>() == null)
            return;

        transform.position = transform.parent.parent.GetComponentInChildren<RFingerID>().transform.position;
        transform.rotation = transform.parent.parent.GetComponentInChildren<RFingerID>().transform.rotation;
        transform.localScale = transform.parent.parent.GetComponentInChildren<RFingerID>().transform.localScale;
    }
}
