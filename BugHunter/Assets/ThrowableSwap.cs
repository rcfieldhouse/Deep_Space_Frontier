using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSwap : MonoBehaviour
{
    public List<Transform> Icons;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.TabThrowable += ChooseIcon;

        for (int i = 0; i<transform.childCount;i++)
        {
            Icons.Add(transform.GetChild(i));
        }
    }
    
    private void ChooseIcon()
    {
        //if (Grenade.isAvailable)
        //{
        //
        //}
    }
}
