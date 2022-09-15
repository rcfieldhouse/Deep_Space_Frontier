using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    // Start is called before the first frame update
    //Match each recticle with The Coresponding Weapon
   public GameObject[] WeaponArray;
    public GameObject[] RecticleArray; 
    private int WeaponChoice = 0;
    void Start()
    {
        for (int i = 0; i < WeaponArray.Length; i++)
        {
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);
        }

        RecticleArray[WeaponChoice].SetActive(true);
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
                RecticleArray[i].SetActive(false);
                WeaponArray[i].SetActive(false);
            }

            RecticleArray[num].SetActive(true);
            WeaponArray[num].SetActive(true);
            WeaponChoice = num;
        }
       
    }
}
