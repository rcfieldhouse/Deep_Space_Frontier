using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubShip : MonoBehaviour
{
    [Range(0f, 60f)] public float JourneyTimer;
    private float timer = 0.0f;
   public Transform target,Start,hint1,hint2;
    Vector3 p0,p1,p2,p3;
    private void Awake()
    {
       p0 = Start.position;
       p1 = hint1.position;
       p3 = target.position;
       p2 = hint2.position;
    }
    Vector3 GetBezierPosition(float t)
    {

        // here is where the magic happens!
        return Mathf.Pow(1f - t, 3f) * p0 + 3f * Mathf.Pow(1f - t, 2f) * t * p1 + 3f * (1f - t) * Mathf.Pow(t, 2f) * p2 + Mathf.Pow(t, 3f) * p3;
    }
    // Update is called once per frame
    void Update()
    { timer += Time.deltaTime;
       
        float var = timer / JourneyTimer;
        float var2 = timer+Time.deltaTime / JourneyTimer;
        Vector3 LookAtTransform = GetBezierPosition(var2);
        if (timer < JourneyTimer)
        {
            transform.LookAt(LookAtTransform);
            transform.Rotate(new Vector3(-90.0f, -90.0f, 0.0f));
            transform.position = GetBezierPosition(var);
        }
      
    }

}
