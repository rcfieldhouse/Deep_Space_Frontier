using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandReloadAnim : MonoBehaviour
{
    public GameObject Shell;
    public Transform LoadPos,CatchPos;
    public Vector3 OriginalPos;
    public Quaternion OriginalRot;
    private float Timer=0.5f, Journey=0.0f;
    private bool Reloading = false,ReloadToggle=true;
    public void SetIsReloading(bool var)
    {
        Reloading = var;

    }
    private void Awake()
    {
        Timer = GetComponentInParent<WeaponInfo>()._reloadTimer/2.0f;
        Invoke(nameof(GetHandPos), 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(ReloadToggle==true)
            Journey+=Time.deltaTime;
        else 
        Journey-=Time.deltaTime;

        if (Journey > Timer)
        {
            Shell.SetActive(false);
            ReloadToggle = false;
        }

        if (Journey < 0.0f)
        {
            Shell.SetActive(true);
            ReloadToggle = true;
        }
          
        if (Reloading==true)
        {
            transform.position = Vector3.Lerp(CatchPos.position, LoadPos.position, Journey / Timer);
            transform.rotation = Quaternion.Lerp(CatchPos.rotation, LoadPos.rotation, Journey / Timer);
        } 
        else
        {
            transform.localPosition = OriginalPos;
            transform.localRotation = OriginalRot;
            Shell.SetActive(false);
        }
    }
    void GetHandPos()
    {
        OriginalPos = transform.localPosition;
        OriginalRot = transform.localRotation;
    }
}
