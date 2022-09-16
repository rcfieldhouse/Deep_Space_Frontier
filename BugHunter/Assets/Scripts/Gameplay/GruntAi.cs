using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAi : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider CC;
    public GameObject player; 

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        CC=GetComponent<CapsuleCollider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
