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
        if (gunAnimator != null&&gunAnimator.gameObject.GetComponent<WeaponInfo>().GetCanReload()==true)
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
