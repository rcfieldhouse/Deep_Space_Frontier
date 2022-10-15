using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is the class that makes the guns do their recoil anim and should be attached to the weapon camera

public class Recoil : MonoBehaviour
{
    [SerializeField] private bool _CanShoot = true;
    [SerializeField] private bool HasAmmo = true;
    [Range(0, 1)] private float Weight;
    private float animTime;
    private float UpTime, DownTime;
    private WaitForSeconds foo = new WaitForSeconds(0.07f);
    private WaitForSeconds bar = new WaitForSeconds(0.93f);

    private Vector3 RecoilPos,BasePos, RecoilPosSet;

    private Quaternion RecoilRot, BaseRot,RecoilRotSet;

    public float RecoilFOV,BaseFOV, RecoilFOVSet, RecoilFOVSetter;

    Camera Camera;
    // Start is called before the first frame update
    void Start()
    { 
   //basic position data
        BaseFOV = 60.0f;
        BaseRot = Quaternion.Euler(0.0f, 0f, 0);
        BasePos = new Vector3(0.0f, 0.0f, 0.0f);
    //to base each weapons recoil off of
        RecoilRotSet = Quaternion.Euler(20.0f, 0f, 0);
        RecoilPosSet = new Vector3(0.0f, 0.1f, 0.2f);
        RecoilFOVSet = 20.0f;
    //action assignments
        PlayerInput.Shoot += StartShot;
        WeaponSwap.BroadCastWeaponRecoilData += SetAnimProperties;
        WeaponInfo.maginfo += getIfMagHasAmmo;
        Weight = 1.25f;
        SetAnimProperties(new Vector4(1.0f, 1.0f, 1.0f, Weight));

        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void getIfMagHasAmmo(bool var)
    {
        HasAmmo = var;
    }
    private void SetAnimProperties(Vector4 vec)
    {
        //x=RecoilRotIntensity
        //y=RecoilOffsetIntensity
        //z=RecoilTimer
        //w=snapiness
        _CanShoot = true;
        //rotation 
        RecoilRot = Quaternion.Lerp(BaseRot, RecoilRotSet, vec.y);

        //position
        RecoilPos = RecoilPosSet * vec.x;
     //   RecoilFOVSet= BaseFOV+ RecoilFOVSet *(1-vec.x);
        RecoilFOVSetter = RecoilFOVSet - RecoilFOVSet*(1-vec.y);
        //snappiness and timing
        animTime = vec.z - 0.07f;
        UpTime = vec.w * animTime;
        DownTime = animTime - UpTime;
    }
    private void StartShot()
    {
        if (_CanShoot==true&& HasAmmo==true)
        StartCoroutine(DoRecoil());

        _CanShoot = false;      
    }
    private IEnumerator DoRecoil()
    {
        //delay for line renderer
        //yield return foo;
        BaseFOV = GetComponent<Camera>().fieldOfView;
        RecoilFOV = BaseFOV - RecoilFOVSetter;
        //gun going up anim
        float elapsed = 0f;

        while (elapsed < UpTime)
        {
            elapsed += Time.deltaTime;
            Camera.fieldOfView = Mathf.Lerp(BaseFOV, RecoilFOV, elapsed / UpTime);
            gameObject.transform.localPosition =  Vector3.Lerp(BasePos, RecoilPos, elapsed / UpTime);
            gameObject.transform.localRotation = Quaternion.Slerp(BaseRot, RecoilRot, elapsed / UpTime);
            yield return null;
        }

        //gun going down anim
        elapsed = 0f;
        while (elapsed < DownTime)
        {
            elapsed += Time.deltaTime;
           Camera.fieldOfView = Mathf.Lerp(RecoilFOV, BaseFOV, elapsed / DownTime);
            gameObject.transform.localPosition = Vector3.Lerp(RecoilPos, BasePos, elapsed / DownTime);
            gameObject.transform.localRotation = Quaternion.Slerp(RecoilRot, BaseRot, elapsed / DownTime);
            yield return null;
        }

        _CanShoot = true;

       // yield return bar;
     
    }
 
}
