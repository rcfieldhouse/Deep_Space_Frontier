using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSwap : MonoBehaviour
{
    public List<Transform> Icons;
    private int Selection = 0;
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
        Selection++;
        if (Selection > 1)
            Selection = 0;
        for (int i=0; i < Icons.Count; i++)
        {
            Icons[i].gameObject.SetActive(false);
        }
        Icons[Selection].gameObject.SetActive(true);
    }
}
