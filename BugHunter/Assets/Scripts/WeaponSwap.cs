using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject[] WeaponArray;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWeapon(int num)
    {
          for (int i = 0; i < WeaponArray.Length; i++)
        {
            WeaponArray[i].SetActive(false);
        }      
      
        WeaponArray[num].SetActive(true);
    
    }
}
