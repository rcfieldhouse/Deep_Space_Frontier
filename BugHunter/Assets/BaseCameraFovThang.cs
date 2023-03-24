using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BaseCameraFovThang : MonoBehaviour
{

    public WeaponSwap WeaponHolder;
    public PlayerInput PlayerInput;
    private CinemachineVirtualCamera VCamera;
    private float FOVChange = 0,BaseZoom=0,iterator;
    private bool _IsADS = false,Running=false;
    // Start is called before the first frame update
    private void Awake()
    {
        VCamera = GetComponent<CinemachineVirtualCamera>();
        BaseZoom = VCamera.m_Lens.FieldOfView;
        PlayerInput.ADS += ChangeFOV;
        PlayerInput.Sprinting += SetRunning;
        WeaponSwap.BroadcastZoom += ListenForNewZoom;
    }
    private void OnDestroy()
    {
        PlayerInput.Sprinting -= SetRunning;
        PlayerInput.ADS -= ChangeFOV;
        WeaponSwap.BroadcastZoom -= ListenForNewZoom;
    }
    private void ListenForNewZoom(float num)
    {
        FOVChange = (BaseZoom) - num * 6;
    }
    private void ChangeFOV(bool var)
    {
        iterator = 0f;
        _IsADS = var;
    }
    public void SetRunning(bool var)
    {
        Running = var;
    }
    private void Update()
    {
        if (Running) return;
        iterator += Time.deltaTime;
        if (_IsADS == true)
            VCamera.m_Lens.FieldOfView = Mathf.Lerp(BaseZoom, FOVChange, iterator * 12);        
        else if (_IsADS == false)
            VCamera.m_Lens.FieldOfView = Mathf.Lerp(FOVChange, BaseZoom, iterator * 12);
        

    }
}
