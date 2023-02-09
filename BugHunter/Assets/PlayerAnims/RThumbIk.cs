using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RThumbIk : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.parent.GetComponentInChildren<RThumbID>())
            return;

        transform.position = transform.parent.parent.GetComponentInChildren<RThumbID>().transform.position;
        transform.rotation = transform.parent.parent.GetComponentInChildren<RThumbID>().transform.rotation;
        transform.localScale = transform.parent.parent.GetComponentInChildren<RThumbID>().transform.localScale;
    }
}
