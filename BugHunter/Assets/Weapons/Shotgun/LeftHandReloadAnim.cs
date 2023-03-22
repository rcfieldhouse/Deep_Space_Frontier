using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandReloadAnim : MonoBehaviour
{
    public GameObject Shell;
    public Transform LoadPos,CatchPos;
    private Transform OriginalPos;
    private float Timer=0.5f, Journey=0.0f;
    private bool Reloading = false,ReloadToggle=true;
    public void SetIsReloading(bool var)
    {
        Reloading = var;
        Debug.Log("reloading shotgun");
    }
    private void Awake()
    {
        OriginalPos = transform;
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
           Debug.Log(Journey/Timer);
            transform.position = Vector3.Lerp(CatchPos.position, LoadPos.position, Journey / Timer);
            transform.rotation = Quaternion.Lerp(CatchPos.rotation, LoadPos.rotation, Journey / Timer);
        } 
        else
        {
            transform.position = OriginalPos.position;
            transform.rotation = OriginalPos.rotation;
            Shell.SetActive(false);
        }
    }
}
