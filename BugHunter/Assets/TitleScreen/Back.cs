using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void GoBack()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {

            transform.parent.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < transform.parent.childCount; i++)
        {

            transform.parent.GetChild(i).gameObject.SetActive(true);
        }
    } 
}
