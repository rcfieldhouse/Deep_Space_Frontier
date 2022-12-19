using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectVendorUI : MonoBehaviour
{
    public List<GameObject> Menus;
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Menus.Add(transform.GetChild(i).gameObject);
            Menus[i].SetActive(false);
        }

        NPC.SelectUI += SelectUI;
    }

    private void SelectUI(string name, bool var)
    {
      for (int i = 0; i < Menus.Count; i++)
        {
            if (name == Menus[i].name)
            {
                Menus[i].SetActive(var);
            }
        }
    }
}
