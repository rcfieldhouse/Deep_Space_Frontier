using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ZoomIn : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private bool isScoped = false;
    [SerializeField]  private VolumeProfile volumeProfile;
    public DepthOfField _DepthOfField;
    private int choice=0;
    public PlayerInput PlayerInput;

    private void Awake()
    {
        WeaponSwap.BroadcastChoice += SetWeapon;
        PlayerInput.ADS += HandleAim;
        DepthOfField dof;
        if (volumeProfile.TryGet(out dof)) { _DepthOfField = dof; }
    }
    private void OnDestroy()
    {
        WeaponSwap.BroadcastChoice -= SetWeapon;
        PlayerInput.ADS -= HandleAim;
    }
    public void HandleAim(bool isAiming)
    {
        if (isAiming && choice == 0 && GetComponent<WeaponSwap>().WeaponArray[0].name == "Sniper")
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
            _DepthOfField.active = true;
        }
        else
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
            _DepthOfField.active = false;
        }

    }
    private void SetWeapon(int foo)
    {
        //foo is the index in the array DANTE
        choice = foo;
    }
}
