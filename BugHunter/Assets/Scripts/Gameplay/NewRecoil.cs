using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecoil : MonoBehaviour
{
    [SerializeField] private Vector3 currentRotation;
    [SerializeField] private Vector3 targetRotation;

    [SerializeField]private float RecoilX;
    [SerializeField]private float RecoilY;
    [SerializeField]private float RecoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    public float AimCorrection=0,baseAim=0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Shoot += RecoilStart;

    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        if (targetRotation.x > -0.1f)
        {
            targetRotation = Vector3.zero;
            currentRotation = Vector3.zero;
        }
        if (targetRotation != Vector3.zero)
        {
       
        }
       // Debug.Log(GameObject.Find("CameraManager").GetComponent<Transform>().rotation.eulerAngles.x);
    }

    public void RecoilStart()
    {
        if (targetRotation == Vector3.zero)
        {
            
            baseAim = GameObject.Find("CameraManager"). GetComponent<Transform>().rotation.eulerAngles.x;
            Debug.Log(baseAim);
        }
       
       
        targetRotation += new Vector3(RecoilX, Random.Range(-RecoilY, RecoilY), Random.Range(-RecoilZ, RecoilZ));
    }
}
