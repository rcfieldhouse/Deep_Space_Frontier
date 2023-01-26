using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    LineRenderer Line;
    [Range(0, -50)] public int Damage;
    [Range(0, 5)] public float ShootTime;
    public bool _CanShoot = true;
    public GameObject Target=null;
    public Transform BulletEmitter;

    private WaitForSeconds shootDelay;
    // Start is called before the first frame update
    private float AngleDifferenceX = 0;
    private float AngleDifferenceZ = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        SetTarget(transform.GetChild(transform.childCount-1).gameObject);
        shootDelay = new WaitForSeconds(ShootTime);
        Target = null;
        Line = gameObject.AddComponent<LineRenderer>();
        Line.startWidth = 0.25f;
        Line.endWidth = 0.25f;
        Line.enabled = false;
    }
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Target =other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {  
            Target = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Target = null;
        }
        }
    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(BulletEmitter.position, transform.GetChild(0).forward, out hit, 20.0f))
        {
            if (isActiveAndEnabled  == true&&_CanShoot==true)
            {
                StartCoroutine(ShotEffect());
                if (hit.collider.tag == "Enemy")
                {
                    Line.SetPosition(1, hit.point);
                    hit.collider.gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
                    if (hit.collider.gameObject.GetComponent<HealthSystem>().GetHealth() <= 0)
                    {
                        hit.collider.gameObject.tag = "Dead";
                        Target = null;
                    }
                       
                }

            }

        }
    }


private IEnumerator ShotEffect()
{
        _CanShoot = false;
        Line.SetPosition(0, BulletEmitter.position);
        Line.SetPosition(1, BulletEmitter.position + transform.GetChild(0).forward * 20.0f);
        // Turn on our line renderer
        Line.enabled = true;
        yield return new WaitForSeconds(0.2f);
        Line.enabled = false;

        yield return shootDelay;

        _CanShoot = true;
        // Deactivate our line renderer after waiting
        Line.enabled = false;
}
private void Update()
    {
        if (Target != null)
        {
            if(Target.GetComponentInChildren<MeshRenderer>()!=null)
            transform.GetChild(0).gameObject.transform.LookAt(Target.GetComponentInChildren<MeshRenderer>().transform);
           if(Target.GetComponentInChildren<SkinnedMeshRenderer>()!=null)
                transform.GetChild(0).gameObject.transform.LookAt(Target.GetComponentInChildren<SkinnedMeshRenderer>().transform);

            Shoot();
        }
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
      

        transform.GetChild(0).gameObject.transform.localPosition = new Vector3((1 - (AngleDifferenceX / 180.0f)) * -0.25f, -0.0f, 0.125f * (AngleDifferenceZ / 90.0f));
    }
}
