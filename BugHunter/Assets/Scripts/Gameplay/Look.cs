using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public GameObject PlayerViewPoint;
    private Vector3 offset = new Vector3(-0.02f, 0.04f, 0.0f);
    private PlayerInput Player;
    bool Aiming = false;
    // Start is called before the first frame update
    void Awake()
    {
        Player = transform.parent.GetChild(0).GetComponent<PlayerInput>();
        Player.Look += Aim;
    }
    public void SetIsPaused(bool var)
    {
        if(var==true)
            Player.Look -= Aim;
        else if (var==false)
            Player.Look += Aim;
    }
    // Update is called once per frame
 
    private void Aim(Quaternion quaternion)
    {
        gameObject.transform.position = PlayerViewPoint.transform.position;
        if (Aiming==true) return;
        gameObject.transform.localRotation = quaternion;
      
    }
    private void Update()
    {
        gameObject.transform.position = PlayerViewPoint.transform.position;
    }
    public void AimAssist(Vector3 vec)
    {
        Aiming = true;
        gameObject.transform.LookAt(vec);
    }
   public void restoreAim()
    {
        Aiming = false;
    }
    private void OnDisable()
    {
        Player.Look -= Aim;
    }
    private void OnDestroy()
    {
        Player.Look -= Aim;
    }
}
