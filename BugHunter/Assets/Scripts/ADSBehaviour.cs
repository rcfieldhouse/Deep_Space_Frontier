using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSBehaviour : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;
    private float defaultZoom;

    // Start is called before the first frame update
    void Start()
    {
        defaultZoom = Cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            zoomIn();
        }
        else zoomOut();
        
        
    }

    void zoomIn()
    {
        Cam.fieldOfView = defaultZoom/2;
    }

    void zoomOut()
    {
        Cam.fieldOfView = defaultZoom;
    }
}
