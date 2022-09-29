using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class AmmoChangeUI : MonoBehaviour
{

    private TextMeshProUGUI ammoCount;
    public List<Mag> magazineSize;
    public WeaponSwap currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = GetComponent<TextMeshProUGUI>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        int i = currentWeapon.GetWeaponNum();
       // Debug.LogWarning(currentWeapon.GetWeaponNum());
         ammoCount.text = magazineSize[i].GetMag().ToString() +
           " / "+ magazineSize[i].GetReserveAmmo().ToString();
    }

}
