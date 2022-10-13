using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunZoom : MonoBehaviour
{
    private Camera Camera;
    private float BaseZoom, NewZoom = 20, iterator;
    private bool isADS = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.ADS += Zoom;
        WeaponSwap.BroadcastADSZoom += SetZoom;

        Camera = GetComponent<Camera>();

        BaseZoom = Camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        iterator += Time.deltaTime;
     if (isADS == true)
     {
         Camera.fieldOfView = Mathf.Lerp(BaseZoom, NewZoom,iterator*12) ;
     }
     else if (isADS == false)
     {
         Camera.fieldOfView = Mathf.Lerp(NewZoom, BaseZoom, iterator*12);
     }
       
         
    }
    private void SetZoom(float num)
    {
       NewZoom = (BaseZoom-30.0f)-num;
    }
    private void Zoom(bool var)
    {
        iterator = 0f;
        isADS = var;
        
    }
}
