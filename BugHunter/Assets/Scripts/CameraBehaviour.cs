using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
public class CameraBehaviour : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject player;
    private bool _manuallyFollow, interupted, interruptPossible;
    public bool _canShoot, ResetAim = false;
    private bool HasAmmo=true;

    public Vector3 offset = new Vector3(-0.02f, 0.04f, 0.0f);
    public Quaternion NoRot = Quaternion.Euler(0.0f, 0f, 0f);
    public Quaternion BaseRot = Quaternion.Euler(0.0f, 0f, 0f);
    public Quaternion RecoilRotSetter = Quaternion.Euler(0.0f, 0f, 0f);
    public Quaternion RecoilRotSet = Quaternion.Euler(20.0f, 0f, 0),RecoilRot,RecoilRotation;
   [SerializeField] private float animTime = 0, UpTime, DownTime,Timer=0;


    // Start is called before the first frame update
    void Start()
    {
        _canShoot = true;
        WeaponSwap.BroadCastWeaponRecoilData += SetAnimProperties;
        PlayerInput.Shoot += ShootCameraWork;
        WeaponInfo.maginfo += getIfMagHasAmmo;
       // PlayerInput.Chamber += cancel;
     VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        VirtualCamera.Follow = player.transform;

        RecoilRotSet = Quaternion.Euler(20.0f, 0f, 0);

    }
    private void cancel()
    {
        if (interruptPossible==true)
        { 
        interupted = true;
        }
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

        RecoilRotation = Quaternion.Euler(0f, 0f, 0);
        //rotation 
        RecoilRot = Quaternion.Lerp(BaseRot, RecoilRotSet, vec.y);
        RecoilRotSetter = RecoilRot;
        //snappiness and timing
        animTime = vec.z - 0.07f;
        UpTime = vec.w * animTime;
        DownTime = animTime - UpTime;
    }
    private void ShootCameraWork()
    {
        Debug.Log("called");
        // RecoilRot = Quaternion.Euler(20.0f, 0f, 0);
        if (_canShoot == true &&HasAmmo==true) 
        {
            if (interruptPossible == true) { 
            cancel();
                _manuallyFollow = true;
                _canShoot = false;
            }

            else if (interruptPossible == false)
            {
                StopCoroutine(DoRecoil());
                StartCoroutine(DoRecoil());
                _manuallyFollow = true;
                VirtualCamera.Follow = null;
                _canShoot = false;
            }
        
        }
      
    }
    private void ResetCams()
    {
        gameObject.transform.localRotation = BaseRot;
        _canShoot = true;
        interruptPossible = false;
        RecoilRot = RecoilRotSetter;
        BaseRot = Quaternion.Euler(0.0f, 0f, 0f);
        _manuallyFollow = false;
        VirtualCamera.Follow = player.transform;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("player "+player.transform.localRotation+ "camera" +  gameObject.transform.localRotation);
      
    }
    private IEnumerator DoRecoil()
    {
        //delay for line renderer
        //yield return foo;

        //gun going up anim
        float elapsed = 0f;

        while (elapsed < UpTime)
        {
            elapsed += Time.deltaTime;
            RecoilRotation = Quaternion.Slerp(BaseRot, RecoilRot, elapsed / UpTime);
         
            yield return null;
        }
        interruptPossible = true;
        //gun going down anim
        elapsed = 0f;
        while (elapsed < DownTime*1.5)
        {
            if (interupted == true)
            {
                RecoilRot = RecoilRotation * RecoilRotSetter;
                BaseRot = RecoilRotation;
                interupted = false;
                StopCoroutine(DoRecoil());
                StartCoroutine(DoRecoil());
                break;
            }

          if (elapsed > (DownTime))
            {
                _canShoot = true;
            }
            elapsed += Time.deltaTime;
            RecoilRotation = Quaternion.Slerp(RecoilRot, NoRot, (elapsed / DownTime)/1.5f);
            yield return null;
            }
        if (RecoilRotation == NoRot)
        {
            ResetCams();
        }

        interruptPossible = false;
   // 
        // yield return bar;

    }
    private void LateUpdate()
    {
        if (_manuallyFollow == true)
        {
            
            // Debug.Log(RecoilRotation);
            transform.rotation = player.transform.rotation * Quaternion.Inverse(RecoilRotation) ;
            transform.position = player.transform.position+ ( player.transform.localRotation*offset);
    }
        }
      
}
