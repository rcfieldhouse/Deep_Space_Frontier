using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
  public  Camera Camera;
    public PlayerInput PlayerInput;
    bool isAim;
    private void Awake()
    {
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

        if (Physics.Raycast(RayOrigin, Camera.transform.forward, out Hit, 250)&& Hit.transform.tag == "Enemy"&&isAim)
        {
                Debug.Log(Hit.transform.tag);
            PlayerInput.AimAssist = true;
            return;
        }
        PlayerInput.AimAssist = false;
    }
}
