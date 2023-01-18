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
        Gun1 = GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray[Choice1];
        Gun2 = GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray[Choice2];
        Reticle1 = GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray[Choice1];
        Reticle2 = GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray[Choice2];

        Icon1 = GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons[Choice1];
        Icon2 = GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons[Choice2];

        AC1 = GameObject.Find("Ammo Counter").GetComponent<AmmoChangeUI>().magazineSize[Choice1];
        AC2 = GameObject.Find("Ammo Counter").GetComponent<AmmoChangeUI>().magazineSize[Choice2];
        for (int i =0; i < GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Count; i++)
        {
            if (i != Choice1 && i != Choice2)
            {
               GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons[i].gameObject.SetActive(false);
                Destroy(GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray[i].gameObject);
                GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray[i].gameObject.SetActive(false);
            }
           //if (GameObject.Find("Crosshairs").transform.GetChild(i))
           //GameObject.Find("Crosshairs").transform.GetChild(i).gameObject.SetActive(false);
        }


        GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Clear();
        GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Clear();
        GameObject.Find("Ammo Counter").GetComponent<AmmoChangeUI>().magazineSize.Clear();
        GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray.Clear();

        GameObject.Find("Ammo Counter").GetComponent<AmmoChangeUI>().magazineSize.Add(AC1);
        GameObject.Find("Ammo Counter").GetComponent<AmmoChangeUI>().magazineSize.Add(AC2);

        GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Add(Icon1);
        GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Add(Icon2);     
        
       GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Add(Gun1);
       GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Add(Gun2);

       GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray.Add(Reticle1);
       GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray.Add(Reticle2);

        Destroy(this);
    }
}
