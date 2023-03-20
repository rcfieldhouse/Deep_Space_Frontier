using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunZoom : MonoBehaviour
{
    public GameObject WeaponHolder;
    private Camera Camera;
    private float BaseZoom, ScopedZoom = 20, iterator;
    private bool isADS = false;
    public float ADSTime = 0.0f;
    public PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerInput.ADS += Zoom;    
        WeaponSwap.BroadcastADSZoom += SetZoom;
        WeaponSwap.BroadcastChoice += SetChoice;
        Camera = GetComponent<Camera>();
        ADSTime = 1.0f;
        BaseZoom = Camera.fieldOfView;
    }
   
    private void OnDestroy()
    {
        PlayerInput.ADS-= Zoom;
        WeaponSwap.BroadcastADSZoom -= SetZoom;
    }
    // Update is called once per frame
    void Update()
    {

        if (ADSTime == 0.0f) return;
        if (isADS)
            iterator += Time.deltaTime;
        else if (!isADS)
            iterator -= Time.deltaTime;

        if (iterator > ADSTime)
            iterator = ADSTime;
        if (iterator < 0.0f)
            iterator = 0.0f;


        Camera.fieldOfView = Mathf.Lerp(BaseZoom, ScopedZoom, iterator/ ADSTime);
     
       
         
    }
   private void SetChoice(int choice)
    {
        ADSTime= WeaponHolder.GetComponentInChildren<WeaponInfo>().ADSTime;
    }
    private void SetZoom(float num)
    {
       ScopedZoom = (BaseZoom-30.0f)-num*6;
    }
    private void Zoom(bool var)
    {
        isADS = var;   
    }
}
