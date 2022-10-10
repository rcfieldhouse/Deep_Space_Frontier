using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
public class CameraBehaviour : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject player;
    private bool _manuallyFollow;

    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.5f);
    private Quaternion BaseRot = Quaternion.Euler(0.0f, 0f, 0f);
    public Quaternion RecoilRotSet = Quaternion.Euler(20.0f, 0f, 0),RecoilRot,RecoilRotation;
   [SerializeField] private float animTime = 0, UpTime, DownTime;


    // Start is called before the first frame update
    void Start()
    {
        WeaponSwap.WeaponRecoilData += SetAnimProperties;
        PlayerInput.Shoot += ShootCameraWork;
     VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        VirtualCamera.Follow = player.transform;

        RecoilRotSet = Quaternion.Euler(20.0f, 0f, 0);

    }
    private void SetAnimProperties(Vector4 vec)
    {
        //x=RecoilRotIntensity
        //y=RecoilOffsetIntensity
        //z=RecoilTimer
        //w=snapiness

        //rotation 
        RecoilRot = Quaternion.Lerp(BaseRot, RecoilRotSet, vec.y);

        //snappiness and timing
        animTime = vec.z - 0.07f;
        UpTime = vec.w * animTime;
        DownTime = animTime - UpTime;
    }
    private void ShootCameraWork()
    {
        // RecoilRot = Quaternion.Euler(20.0f, 0f, 0);
        if (_manuallyFollow == false)
        {
            StartCoroutine(DoRecoil());
            _manuallyFollow = true;
            VirtualCamera.Follow = null;
        }
      
    }
    private void ResetCams()
    {
        _manuallyFollow = false;
        VirtualCamera.Follow = player.transform;
    }
    // Update is called once per frame
    void Update()
    {
  
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

        //gun going down anim
        elapsed = 0f;
        while (elapsed < DownTime)
        {
            elapsed += Time.deltaTime;
            RecoilRotation = Quaternion.Slerp(RecoilRot, BaseRot, elapsed / DownTime);
            yield return null;
        }
    ResetCams();
        // yield return bar;

    }
    private void LateUpdate()
    {
        if (_manuallyFollow == true)
        {   
            transform.rotation = player.transform.rotation * Quaternion.Inverse(RecoilRotation) ;
            transform.position = player.transform.position + ( player.transform.localRotation*offset);
    };
        }
      
}
