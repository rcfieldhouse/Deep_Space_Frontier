using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGun : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentGun;
    private WeaponSwap gunHolder;
    private Animator gunAnimator;
    void Start()
    {
        PlayerInput.Reload += Reload;
        gunHolder = GetComponent<WeaponSwap>();
    }

    public void Reload()
    {
        //this is gross 
        gunAnimator = gunHolder.WeaponArray[gunHolder.GetWeaponNum()].GetComponent<Animator>();
        if (gunAnimator != null)
        {
            gunAnimator.Play("Base Layer.Reload", 0, 0);
            Debug.Log("doot");
        }
    }
}
