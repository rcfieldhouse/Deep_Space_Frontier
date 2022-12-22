using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGun : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentGun;
    private WeaponSwap gunHolder;
    private Animator gunAnimator;
    private bool Reloading = false;
    public bool GetIsReloading()
    {
        return Reloading;
    }
    public void SetIsReloading(bool var)
    { 
        Reloading = var;
    }

    void OnEnable()
    {
        PlayerInput.Reload += Reload;
        gunHolder = GetComponent<WeaponSwap>();
    }
    private void OnDestroy()
    {
        PlayerInput.Reload -= Reload;
    }
 
    public void Reload()
    {
        //this is gross 
        gunAnimator = gunHolder.WeaponArray[gunHolder.GetWeaponNum()].GetComponent<Animator>();
        if (gunAnimator != null && gunHolder.WeaponArray[gunHolder.GetWeaponNum()].GetComponent<WeaponInfo>().GetCanReload()==true)
        {        
            gunAnimator.SetBool("Reload", true);
            //gunAnimator.Play("Reload", 0, 0);
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        gunAnimator.SetBool("Reload", false);
    }
}
