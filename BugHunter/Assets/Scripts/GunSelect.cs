using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelect : MonoBehaviour
{
    private GameObject Gun1,Gun2,Icon1,Icon2;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectGun(GameObject Player, int Choice1, int Choice2)
    {
        Gun1 = GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray[Choice1];
        Gun2 = GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray[Choice2];
        GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Clear();
        Icon1 = GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons[Choice1];
        Icon2 = GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons[Choice2];
        GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Clear();

        GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Add(Icon1);
        GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Add(Icon2);      
       GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Add(Gun1);
       GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().WeaponArray.Add(Gun2);
    }
}
