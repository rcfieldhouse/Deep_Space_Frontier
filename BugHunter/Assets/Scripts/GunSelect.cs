using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelect : MonoBehaviour
{
    private GameObject Gun1,Gun2,Icon1,Icon2,Reticle1,Reticle2;
    private WeaponInfo AC1, AC2;
  
    // Start is called before the first frame update
   
    public void SelectGun(GameObject Player, int Choice1, int Choice2)
    {
        AmmoChangeUI ammoChangeUI = GetComponent<GUIHolder>().GUI.GetComponentInChildren<AmmoChangeUI>();
        GunIconUI gunIconUI = GetComponent<GUIHolder>().GUI.GetComponentInChildren<GunIconUI>();
        WeaponSwap weaponSwap = transform.parent.GetChild(1).GetComponentInChildren<WeaponSwap>();

        Debug.Log(weaponSwap.gameObject.name);
        Debug.Log(gunIconUI.gameObject.name);
        Debug.Log(ammoChangeUI.gameObject.name);

        Gun1 = weaponSwap.WeaponArray[Choice1];
        Gun2 = weaponSwap.WeaponArray[Choice2];
        Reticle1 = weaponSwap.GetComponent<WeaponSwap>().RecticleArray[Choice1];
        Reticle2 = weaponSwap.GetComponent<WeaponSwap>().RecticleArray[Choice2];

        Icon1 = gunIconUI.Icons[Choice1];
        Icon2 = gunIconUI.Icons[Choice2];

        AC1 = ammoChangeUI.magazineSize[Choice1];
        AC2 = ammoChangeUI.magazineSize[Choice2];
        for (int i =0; i < weaponSwap.WeaponArray.Count; i++)
        {
            if (i != Choice1 && i != Choice2)
            {
                gunIconUI.Icons[i].gameObject.SetActive(false);
                Destroy(weaponSwap.WeaponArray[i].gameObject);
                weaponSwap.RecticleArray[i].gameObject.SetActive(false);
            }
           //if (GameObject.Find("Crosshairs").transform.GetChild(i))
           //GameObject.Find("Crosshairs").transform.GetChild(i).gameObject.SetActive(false);
        }


        weaponSwap.WeaponArray.Clear();
        gunIconUI.Icons.Clear();
        ammoChangeUI.magazineSize.Clear();
        weaponSwap.GetComponent<WeaponSwap>().RecticleArray.Clear();

        ammoChangeUI.magazineSize.Add(AC1);
        ammoChangeUI.magazineSize.Add(AC2);

        gunIconUI.Icons.Add(Icon1);
        gunIconUI.Icons.Add(Icon2);

        weaponSwap.WeaponArray.Add(Gun1);
        weaponSwap.WeaponArray.Add(Gun2);

        weaponSwap.RecticleArray.Add(Reticle1);
        weaponSwap.RecticleArray.Add(Reticle2);

        Gun1.SetActive(true);
        Reticle1.SetActive(true);
        Icon1.SetActive(true);
        weaponSwap.SetWeapon(0);
        Destroy(this);
    }
}
