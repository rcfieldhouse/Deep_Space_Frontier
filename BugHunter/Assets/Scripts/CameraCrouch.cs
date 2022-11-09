using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCrouch : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(Mathf.Abs(transform.parent.rotation.x));
        transform.localPosition =new Vector3(0.0f,0.5f- (0.5f* (0.7f-Mathf.Abs(transform.parent.rotation.x))),0.0f);
    }
}
