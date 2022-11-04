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
    private DepthOfField _DepthOfField;


    private void Start()
    {
        PlayerInput.ADS += HandleAim;
        DepthOfField dof;
        if (volumeProfile.TryGet(out dof)) { _DepthOfField = dof; }
    }

    public void HandleAim(bool isAiming)
    {
        if (isAiming)
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
}
