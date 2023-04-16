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
    private bool Running = false;
    public WeaponSwap WeaponSwap;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerInput.Sprinting += SetRunning;
        PlayerInput.ADS += Zoom;    
        WeaponSwap.BroadcastADSZoom += SetZoom;
        WeaponSwap.BroadcastChoice += SetChoice;
        Camera = GetComponent<Camera>();
        ADSTime = 1.0f;
        BaseZoom = Camera.fieldOfView;
    }
   
    private void OnDestroy()
    {
        PlayerInput.Sprinting -= SetRunning;
        PlayerInput.ADS-= Zoom;
        WeaponSwap.BroadcastADSZoom -= SetZoom;
        WeaponSwap.BroadcastChoice -= SetChoice;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponentInChildren<ZoomIn>().paused == true)
            return;

        if (ADSTime == 0.0f||Running) return;
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
    public void SetRunning(bool var)
    {
        Running = var;
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
