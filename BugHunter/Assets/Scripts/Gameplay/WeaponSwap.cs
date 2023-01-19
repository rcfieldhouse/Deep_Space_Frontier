using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponSwap : MonoBehaviour
{
    
    // Start is called before the first frame update
    //Match each recticle with The Coresponding Weapon
    public List<GameObject> WeaponArray;
    public List<GameObject> RecticleArray;

    public static Action<Vector4> BroadCastWeaponRecoilData;
    public static Action<Vector3> BroadCastHipRecoil;
    public static Action<Vector3> BroadCastADSRecoil;
    public static Action<int,int> BroadcastWeaponListData;
    public static Action<float> BroadcastADSZoom;
    public static Action<Vector2> BroadcastSnap;
    public static Action<int> BroadcastChoice;

    public ReloadGun reloadGun; 
    private int WeaponChoice = 0;
    void Awake()
    {
        for (int i = 0; i < WeaponArray.Count; i++)
        {
            RecticleArray[i] = GameObject.Find("Crosshairs").GetComponent<CrosshairsHolder>().Crosshairs[i];
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);

        }
       
        RecticleArray[WeaponChoice].SetActive(true);
        WeaponArray[WeaponChoice].SetActive(true);

        reloadGun = GetComponent<ReloadGun>();
        PlayerInput.SwappingWeapon += SetWeapon;
    }
    private void OnDestroy()
    {
        PlayerInput.SwappingWeapon -= SetWeapon;
    }
    // Update is called once per frame
 
    public int GetWeaponNum()
    {
        return WeaponChoice;
    }

    public void SetWeapon(int choice)
    {
        if   (reloadGun.GetIsReloading()== false) { 
        for (int i = 0; i < WeaponArray.Count; i++)
        {
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);
        }
        SetWeaponChoice(choice);
         WeaponChoice = choice;

        //broadcast data to classes that need it 
        BroadcastADSZoom.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetADSZoom());
        BroadCastWeaponRecoilData.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetRecoilInfo());
        BroadcastWeaponListData.Invoke(WeaponChoice, WeaponArray.Count-1);
        BroadCastHipRecoil.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetCameraRecoilInfo(0));
        BroadCastADSRecoil.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetCameraRecoilInfo(1));
        BroadcastSnap.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetSnap());
        BroadcastChoice.Invoke(WeaponChoice);
        }
    }
    public void SetWeaponChoice(int num)
    {
        // if (WeaponArray[num])
        // {
        if (WeaponArray[WeaponChoice].gameObject.GetComponent<WeaponInfo>().GetIsReloading() == false)
        {
            for (int i = 0; i < WeaponArray.Count; i++)
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
            else if (num > WeaponArray.Count - 1)
            {

                RecticleArray[WeaponArray.Count - 1].SetActive(true);
                WeaponArray[WeaponArray.Count - 1].SetActive(true);
                WeaponChoice = WeaponArray.Count - 1;

            }
            else if (num >= 0 && num <= WeaponArray.Count)
            {
                RecticleArray[num].SetActive(true);
                WeaponArray[num].SetActive(true);
                WeaponChoice = num;
            }



        }
    }
}
