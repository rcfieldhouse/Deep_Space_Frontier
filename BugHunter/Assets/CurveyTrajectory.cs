using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveyTrajectory : MonoBehaviour
{
    private float TotalTime=2,CurrentTime=0,Height;
    Vector3 StartPoint, EndPoint;

    public void SetValues(Vector3 Start,Vector3 End, float Heightt)
    {
        StartPoint = Start;
        EndPoint = End;
        Height = Heightt;
    }
    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
 
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
}
