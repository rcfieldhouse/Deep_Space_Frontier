using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunIconUI : MonoBehaviour
{
    public WeaponSwap WeaponSwap;
    public List<GameObject> Icons;
    private int choice = 0;
    // Start is called before the first frame update 
    void Awake()
    {
        WeaponSwap.BroadcastChoice += ChooseIcon;    
    }
    private void OnDestroy()
    {
        WeaponSwap.BroadcastChoice -= ChooseIcon;
    }
    private void ChooseIcon(int Index)
    {
        choice = Index;
        for (int i = 0; i < Icons.Count; i++)
        {
            Icons[i].SetActive(false);
        }
        Icons[Index].SetActive(true);
    }
}
