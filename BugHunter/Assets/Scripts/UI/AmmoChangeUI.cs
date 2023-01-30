using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class AmmoChangeUI : MonoBehaviour
{

    private TextMeshProUGUI ammoCount;
    public List<WeaponInfo> magazineSize;
    public int currentWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        WeaponSwap.BroadcastChoice += SelectWeapon;
        ammoCount = GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < magazineSize.Count; i++)
        {
            magazineSize[i] = transform.parent.parent.GetComponentInChildren<WeaponSwap>().WeaponArray[i].GetComponent<WeaponInfo>();
        }
    }
    private void SelectWeapon(int choice)
    {
        currentWeapon = choice;
    }
    // Update is called once per frame
    void Update()
    {
        int i = currentWeapon;
       // Debug.LogWarning(currentWeapon.GetWeaponNum());
         ammoCount.text = magazineSize[i].GetMag().ToString() +
           " / "+ magazineSize[i].GetReserveAmmo().ToString();
    }

}
