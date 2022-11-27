using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Target=null;
    // Start is called before the first frame update
    private float AngleDifferenceX = 0;
    private float AngleDifferenceZ = 0;
    // Start is called before the first frame update
    private void Awake()
    {
          SetTarget(transform.GetChild(transform.childCount-1).gameObject);
    }
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Target=other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Target = other.gameObject;
        }
    }
    private void Update()
    {
        transform.GetChild(0).gameObject.transform.LookAt(Target.transform);

        AngleDifferenceX = transform.GetChild(0).gameObject.transform.localEulerAngles.y;
        AngleDifferenceZ = transform.GetChild(0).gameObject.transform.localEulerAngles.y;
        if (AngleDifferenceX > 180.0f) AngleDifferenceX = Mathf.Abs(360.0f - AngleDifferenceX);
        if (AngleDifferenceZ > 180.0f) AngleDifferenceZ -= 360.0f;

        if (Mathf.Abs(AngleDifferenceZ) > 90.0f)
        {
            //this took me absolutley forever to figure out
            AngleDifferenceZ = (90.0f * (AngleDifferenceZ / Mathf.Abs(AngleDifferenceZ))) - (AngleDifferenceZ - 90.0f * (AngleDifferenceZ / Mathf.Abs(AngleDifferenceZ)));
        }
        //z 0.125
        
        transform.GetChild(0).gameObject.transform.localPosition = new Vector3((1 - (AngleDifferenceX / 180.0f)) * -0.25f, 0.0f, 0.125f * (AngleDifferenceZ / 90.0f));
    }
}
