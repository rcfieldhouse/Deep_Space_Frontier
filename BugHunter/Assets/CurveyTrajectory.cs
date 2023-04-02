using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveyTrajectory : MonoBehaviour
{
    bool BandAidDone = false;
    public LayerMask WhatIsGround;
    private float TotalTime=2,CurrentTime=0,Height;
    Vector3 StartPoint, EndPoint;
    public float DetectionHeight;
    private void Awake()
    {
        DetectionHeight = 2;
        WhatIsGround = GetComponent<QueenBomb>().WhatIsGround;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(GetComponent<SphereCollider>().bounds.center, GetComponent<SphereCollider>().bounds.center - new Vector3(0.0f, DetectionHeight, 0.0f));
    }
    public void SetValues(Vector3 Start,Vector3 End, float Heightt,float Time)
    {
        StartPoint = Start;
        EndPoint = End;
        Height = Heightt;
        TotalTime += (Time / 10.0f) *(TotalTime/5);
    }
    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (Physics.Raycast(GetComponent<SphereCollider>().bounds.center, Vector3.down, DetectionHeight, WhatIsGround) == true)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            if (BandAidDone == false)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.down*10, ForceMode.Impulse);
            }
            return;
        }
        transform.SetPositionAndRotation(SampleParabola(StartPoint, EndPoint, Height, CurrentTime/TotalTime),Quaternion.identity);
    }
    Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
            return result;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
    }
}
