using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponSwap : MonoBehaviour
{
    // Start is called before the first frame update
    //Match each recticle with The Coresponding Weapon
   public GameObject[] WeaponArray;
    public GameObject[] RecticleArray;
    public static Action<int,int> WeaponListData;

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

        PlayerInput.SwappingWeapon += SetWeapon;
    }

    // Update is called once per frame

    public int GetWeaponNum()
    {
        return WeaponChoice;
    }

    private void SetWeapon(int choice)
    {
        for (int i = 0; i < WeaponArray.Length; i++)
        {
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);
        }
        RecticleArray[choice].SetActive(true);
        WeaponArray[choice].SetActive(true);
        WeaponChoice = choice;

        WeaponListData.Invoke(WeaponChoice, WeaponArray.Length-1);
    }
    public void SetWeaponChoice(int num)
    {
       // if (WeaponArray[num])
       // {
            for (int i = 0; i < WeaponArray.Length; i++)
            {
                RecticleArray[i].SetActive(false);
                WeaponArray[i].SetActive(false);
            }
       // Debug.Log(WeaponArray.Length);

        if (num < 0)
        {
            RecticleArray[0].SetActive(true);
            WeaponArray[0].SetActive(true);
            WeaponChoice = 0;
        }
            else if (num > WeaponArray.Length-1)
        {
          
            RecticleArray[WeaponArray.Length-1].SetActive(true);
            WeaponArray[WeaponArray.Length-1].SetActive(true);
            WeaponChoice = WeaponArray.Length-1;

        }
            else if (num >= 0 && num <= WeaponArray.Length)
        {
            RecticleArray[num].SetActive(true);
            WeaponArray[num].SetActive(true);
            WeaponChoice = num;
        }
         
        //}
       
    }
}
