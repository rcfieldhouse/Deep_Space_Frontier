using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
