using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewThrow : MonoBehaviour
{
    private bool _IsCooking = false;
    private Quaternion Direction;
    public LineRenderer PreviewLine;
    private Vector3 LaunchPoint;
    private Vector3 ThrowForce = (Vector3.forward * 25 + Vector3.up * 5);
    // Start is called before the first frame update
    void Start()
    {
   
        PreviewLine = gameObject.AddComponent<LineRenderer>();
        PreviewLine.positionCount = 10;
        PreviewLine.startWidth = 0.15f;
        PreviewLine.endWidth = 0.15f;
 
    }

    // Update is called once per frame
    void Update()
    {
        Direction = GameObject.Find("WeaponHolder").transform.rotation;
        LaunchPoint= (GameObject.Find("CameraManager").transform.position + Direction * new Vector3(0.0f, 1.75f, 0.0f)) - new Vector3(0.0f, 0.5f, 0.0f);
        if (_IsCooking == true)
        {
            PreviewLine.SetPosition(0, LaunchPoint);
            for (int i = 1; i <= 9; i++)
            {
                PreviewLine.SetPosition(i, PreviewLine.GetPosition(i-1) + 0.1f*(Direction * (ThrowForce + (0.1f*i*Physics.gravity))));
            }
          
        }
    }
    public void CookNade()
    { 
        _IsCooking = true;
        PreviewLine.enabled = true;
    }
    public void Release()
    {
        _IsCooking = false;
        PreviewLine.enabled = false;
    }
}
