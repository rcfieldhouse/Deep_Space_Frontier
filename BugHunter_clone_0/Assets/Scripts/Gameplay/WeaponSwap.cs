using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
public class WeaponSwap : NetworkBehaviour
{
    
    // Start is called before the first frame update
    //Match each recticle with The Coresponding Weapon
    public List<GameObject> WeaponArray;
    public List<GameObject> RecticleArray;

    public event Action <Vector4> BroadCastWeaponRecoilData;
    public event Action<Vector3> BroadCastHipRecoil;
    public event Action<Vector3> BroadCastADSRecoil;
    public event Action<int,int> BroadcastWeaponListData;
    public event Action<float> BroadcastADSZoom,BroadcastZoom;
    public event Action<Vector2> BroadcastSnap;
    public event Action<int> BroadcastChoice;
    public event Action<bool> maginfo, CanShoot;
    private PlayerInput Player;
    public ReloadGun reloadGun; 
    private int WeaponChoice = 0;
    void Awake()
    {
        Player = transform.parent.parent.parent.GetChild(0).GetComponent<PlayerInput>();
        for (int i = 0; i < WeaponArray.Count; i++)
        {
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);

        }
       
        RecticleArray[WeaponChoice].SetActive(true);
        WeaponArray[WeaponChoice].SetActive(true);

        reloadGun = GetComponent<ReloadGun>();
        Player.SwappingWeapon += SetWeapon;
    }
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            Destroy(this);
        base.OnNetworkSpawn();
    }
    private void OnDestroy()
    {
        Player.SwappingWeapon -= SetWeapon;
    }
    // Update is called once per frame
 
    public int GetWeaponNum()
    {
        return WeaponChoice;
    }
    private void Update()
    {
       // Debug.Log(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetCanShoot());
        if (WeaponArray[WeaponChoice].activeInHierarchy == true)
        {
            maginfo.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().hasAmmo());
            CanShoot.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetCanShoot());
        }  
    }
    public GameObject GetWeapon()
    {
        return WeaponArray[WeaponChoice];
    }
    public void SetWeapon(int choice)
    {
        if (WeaponArray[choice] == null)
            return;
        if   (reloadGun.GetIsReloading()== false) { 
        for (int i = 0; i < WeaponArray.Count; i++)
        {
            RecticleArray[i].SetActive(false);
            WeaponArray[i].SetActive(false);
        }
        SetWeaponChoice(choice);
         WeaponChoice = choice;

            //broadcast data to classes that need it 
        BroadcastZoom.Invoke(WeaponArray[WeaponChoice].GetComponent<WeaponInfo>().GetZoom());
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
