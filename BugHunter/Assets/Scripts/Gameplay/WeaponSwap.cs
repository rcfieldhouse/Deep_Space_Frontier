using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponSwap : MonoBehaviour
{
    public static Action<Vector4> BroadCastWeaponRecoilData;
    // Start is called before the first frame update
    //Match each recticle with The Coresponding Weapon
    public List<GameObject> WeaponArray;
    public List<GameObject> RecticleArray;

    public static Action<Vector3> BroadCastHipRecoil;
    public static Action<Vector3> BroadCastADSRecoil;
    public static Action<int,int> BroadcastWeaponListData;
    public static Action<float> BroadcastADSZoom;
    public static Action<Vector2> BroadcastSnap;
    public static Action<int> BroadcastChoice;
    private int WeaponChoice = 0;
    void Start()
    {
        for (int i = 0; i < WeaponArray.Count; i++)
        {
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);
        }

        RecticleArray[WeaponChoice].SetActive(true);
        WeaponArray[WeaponChoice].SetActive(true);

        PlayerInput.SwappingWeapon += SetWeapon;
        StartCoroutine(StartingWeapon());
    }

    // Update is called once per frame
    private IEnumerator StartingWeapon()
    {
        yield return new WaitForEndOfFrame();
        SetWeapon(WeaponChoice);

    }
    public int GetWeaponNum()
    {
        return WeaponChoice;
    }

    private void SetWeapon(int choice)
    {
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
    public void SetWeaponChoice(int num)
    {
       // if (WeaponArray[num])
       // {
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
         
        //}
       
    }
}
