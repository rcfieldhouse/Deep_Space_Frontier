using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunIconUI : MonoBehaviour
{
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
    private void ChooseIcon(int foo)
    {
        choice = foo;
        for (int i = 0; i < Icons.Count; i++)
        {
            Icons[i].SetActive(false);
        }
        Icons[foo].SetActive(true);
    }
}
