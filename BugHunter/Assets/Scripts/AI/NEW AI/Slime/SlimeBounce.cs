using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBounce : MonoBehaviour
{
    [Range(0, 25)] public float JumpHeight=15;
    [Range(0, 3)] public float HeightSlime = 2;
    bool Dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //makes it not jump
       

        if (Physics.Raycast(GetComponent<BoxCollider>().bounds.center - Vector3.down / 12, Vector3.down, HeightSlime, GetComponentInParent<Slime>().WhatIsGround) == true)
            Jump();

        if (GetComponent<HealthSystem>().GetHealth() <= 0)
            Dead = true;
    }
    private void Jump()
    {  // if(Jumped==true)
        GetComponent<Rigidbody>().velocity = Vector3.up * JumpHeight;

        if (Dead == true) GetComponent<Rigidbody>().isKinematic = true;
    }
}
