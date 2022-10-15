using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecoil : MonoBehaviour
{
    public GameObject Player;
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private bool _isAiming = false;

    [SerializeField]private float RecoilX, AimRecoilX;
    [SerializeField]private float RecoilY, AimRecoilY;
    [SerializeField]private float RecoilZ, AimRecoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    private float AimCorrection=0,baseAim=0,num=0;
    private bool RecoilStartPossible = true;
    // Start is called before the first frame update
    private void SetAdsRecoil(Vector3 vec)
    {
       AimRecoilX = -vec.x;
       AimRecoilY = vec.y;
       AimRecoilZ = vec.z;
    }
    private void SetHipRecoil(Vector3 vec)
    {
        RecoilX = -vec.x;
        RecoilY = vec.y;
        RecoilZ = vec.z;
    }
    void Start()
    {
        PlayerInput.Shoot += RecoilStart;
        PlayerInput.Chamber += setRecoilPossible;
        PlayerInput.ADS += SetIsAiming;
        WeaponSwap.BroadCastADSRecoil += SetAdsRecoil;
        WeaponSwap.BroadCastHipRecoil += SetHipRecoil;
        WeaponSwap.BroadcastSnap += SetSnap;
        Player = GameObject.Find("Player");
    }
    private void setRecoilPossible()
    {
        RecoilStartPossible = true;
    }
    // Update is called once per frame

    private void SetIsAiming(bool foo)
    {
        _isAiming = foo;
        //Debug.Log("Isaiming" + _isAiming);
    }

    private void SetSnap(Vector2 vec)
    {
        snappiness = vec.x ;
        returnSpeed = vec.y;
    }
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        if (targetRotation.x > -0.1f)
        {
            AimCorrection = 0;
            targetRotation = Vector3.zero;
            currentRotation = Vector3.zero;
            baseAim = 0;
            RecoilStartPossible = true;
        }
       
        Player.GetComponent<PlayerInput>().MouseInput.y += -AimCorrection * returnSpeed* Time.deltaTime;
        AimCorrection = Mathf.Lerp(AimCorrection, 0.0f, returnSpeed * Time.deltaTime);
       // Debug.Log(baseAim);
       // Debug.Log("AimCorrection "+AimCorrection + " Current Rotation "+targetRotation.x+" Mouse y " + GameObject.Find("CameraManager").GetComponent<Transform>().rotation.eulerAngles.x);
    }

    public void RecoilStart()
    {
        if (RecoilStartPossible==true)
        {
            RecoilStartPossible = false;
                    baseAim = GameObject.Find("CameraManager"). GetComponent<Transform>().rotation.eulerAngles.x;
            if (baseAim > 90)
                baseAim = baseAim - 360.0f;
        }
        if (targetRotation != Vector3.zero)
        {
            num = GameObject.Find("CameraManager").GetComponent<Transform>().rotation.eulerAngles.x;
            if (num > 90)
                num = num - 360.0f;
            AimCorrection = baseAim - num;
        }
        if (_isAiming==false)
        targetRotation += new Vector3(RecoilX, Random.Range(-RecoilY, RecoilY), Random.Range(-RecoilZ, RecoilZ));

        if(_isAiming == true)
        targetRotation += new Vector3(AimRecoilX, Random.Range(-AimRecoilY, AimRecoilY), Random.Range(-AimRecoilZ, AimRecoilZ));
    }
}
