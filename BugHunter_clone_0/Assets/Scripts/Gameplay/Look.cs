using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Look : NetworkBehaviour
{
    public GameObject PlayerViewPoint;
    private Vector3 offset = new Vector3(-0.02f, 0.04f, 0.0f);
    private PlayerInput Player;
    bool Aiming = false;
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            Destroy(this);
        base.OnNetworkSpawn();
    }

    void Awake()
    {
        Player = transform.parent.GetChild(0).GetComponent<PlayerInput>();
        Player.Look += Aim;
    }

    public void SetIsPaused(bool var)
    {
        if (var==true)
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
    public void AimAssist(Vector3 vec)
    {
        Debug.Log("true");
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
