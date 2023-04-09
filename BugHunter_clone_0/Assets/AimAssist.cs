using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
  public  Camera Camera;
    public PlayerInput PlayerInput;
    bool isAim;
   public bool AIMHacks ;
    private void Awake()
    {
        AIMHacks = false;
        PlayerInput.ADS += SetAiming;
    }
    private void OnDestroy()
    {
        PlayerInput.ADS -= SetAiming;
    }
    void SetAiming(bool var)
    {
        isAim = var;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit Hit;

        if (Physics.Raycast(RayOrigin, Camera.transform.forward, out Hit, 250) && Hit.transform.tag == "Enemy" && isAim)
        {
            if (AIMHacks == true)
            {
                if (Hit.transform.GetComponent<SphereCollider>() != null)
                {
                    Debug.Log((Hit.transform.rotation * Hit.transform.GetComponent<SphereCollider>().center) + Hit.transform.position);
                    GetComponent<Look>().AimAssist((Hit.transform.rotation * Hit.transform.GetComponent<SphereCollider>().center) + Hit.transform.position);
                }
                else
                    GetComponent<Look>().AimAssist(Hit.transform.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center);
            }
            // Debug.Log(Hit.transform.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center);

            PlayerInput.AimAssist = true;
            return;
        }
        else GetComponent<Look>().restoreAim();
        PlayerInput.AimAssist = false;
    }
}
