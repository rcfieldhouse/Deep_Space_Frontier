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
    private bool _IsADS = false;
    // Start is called before the first frame update
    private void Awake()
    {
        VCamera = GetComponent<CinemachineVirtualCamera>();
        BaseZoom = VCamera.m_Lens.FieldOfView;
        PlayerInput.ADS += ChangeFOV;
        WeaponSwap.BroadcastZoom += ListenForNewZoom;
    }
    private void ListenForNewZoom(float num)
    {
        Debug.Log(num);
        FOVChange = (BaseZoom) - num * 6;
    }
    private void ChangeFOV(bool var)
    {
        iterator = 0f;
        _IsADS = var;
    }
    private void Update()
    {
        iterator += Time.deltaTime;
        if (_IsADS == true)
            VCamera.m_Lens.FieldOfView = Mathf.Lerp(BaseZoom, FOVChange, iterator * 12);        
        else if (_IsADS == false)
            VCamera.m_Lens.FieldOfView = Mathf.Lerp(FOVChange, BaseZoom, iterator * 12);
        

    }
}
