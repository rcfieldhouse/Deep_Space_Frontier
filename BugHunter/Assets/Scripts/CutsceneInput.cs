using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneInput : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public void walk()
    {
        GetComponent<Animator>().Play("RifleWalkForward");
    }
    // Start is called before the first frame update
    void Awake()
    {
              
        
    }

     
    // Update is called once per frame
    void Update()
    {
        walk();
        GetComponent<Rigidbody>().velocity = Vector3.left * moveSpeed;
    }
}
