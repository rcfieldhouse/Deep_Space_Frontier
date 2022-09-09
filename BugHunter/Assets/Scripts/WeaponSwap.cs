using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject[] WeaponArray;
    private int WeaponChoice = 0;
    void Start()
    {
        WeaponArray[WeaponChoice].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetWeaponNum()
    {
        return WeaponChoice;
    }
    public void SetWeapon(int num)
    {
        if (WeaponArray[num] != null)
        {
            for (int i = 0; i < WeaponArray.Length; i++)
            {
                WeaponArray[i].SetActive(false);
            }

            WeaponArray[num].SetActive(true);
            WeaponChoice = num;
        }
       
    }
}
