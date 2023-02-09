using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LThumbIK : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (transform.parent.parent.GetComponentInChildren<LThumbID>() == null)
            return;

        transform.position = transform.parent.parent.GetComponentInChildren<LThumbID>().transform.position;
            transform.rotation = transform.parent.parent.GetComponentInChildren<LThumbID>().transform.rotation;
            transform.localScale = transform.parent.parent.GetComponentInChildren<LThumbID>().transform.localScale;
    }
}
