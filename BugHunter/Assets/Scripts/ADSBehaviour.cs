using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSBehaviour : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [Range(0 , 3)] [SerializeField] private float scopeInRate = 0.1f;
    [Range(0 , 3)] [SerializeField] private float scopeOutRate = 0.3f;
    private float defaultZoom;
    private float currentZoom;
    private float t = 0f;


    // Start is called before the first frame update
    void Start()
    {
        defaultZoom = Cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        handleZoom();
 
    }

    void handleZoom()
    {

        if (Input.GetButton("Fire2"))
        {
            t += scopeInRate * Time.deltaTime;
            if (t >= 1) t = 1;
        }

        else
        {
            t -= scopeOutRate * Time.deltaTime;
            if (t <= 0) t = 0;
        }

        Cam.fieldOfView = Mathf.Lerp(defaultZoom, defaultZoom / 2, t);
    }
}
