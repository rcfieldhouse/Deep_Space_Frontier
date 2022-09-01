using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSBehaviour : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private Transform Gun;
    [Range(0 , 3)] [SerializeField] private float scopeInRate = 2f;
    [Range(0 , 3)] [SerializeField] private float scopeOutRate = 2f;
    [Range(0, 179)] [SerializeField] private float targetZoom = 40f;
    [SerializeField] private Vector3 targetGunPosition = new Vector3(0f, -0.254000008f, 0.861999989f);
    [SerializeField] private Vector3 targetGunRotation = new Vector3(10f, 30f, 0f);



    private float defaultZoom;
    private Vector3 defaultGunPosition;
    private Vector3 defaultGunRotation;
    private float t = 0f;


    // Start is called before the first frame update
    void Start()
    {
        defaultZoom = Cam.fieldOfView;
        defaultGunPosition = Gun.localPosition;
        defaultGunRotation = new Vector3(0,180,0);
    }

    // Update is called once per frame
    void Update()
    {

        HandleZoom();
 
    }

    void HandleZoom()
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

        Cam.fieldOfView = Mathf.Lerp(defaultZoom, targetZoom, t);

        Gun.localPosition = Vector3.Lerp(defaultGunPosition, targetGunPosition, t);
        Gun.localRotation = Quaternion.Euler(Vector3.Lerp(defaultGunRotation, targetGunRotation, t));
        //Quaternion.Lerp(quat1,quat2)
    }
}
