using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenCutsceneMid : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public void move()
    {
        GetComponent<Animator>().Play("MoveFowardFinal");
    }
    // Start is called before the first frame update
    void Awake()
    {


    }


    // Update is called once per frame
    void Update()
    {
        move();
        //GetComponent<Rigidbody>().velocity = Vector3.down * moveSpeed;
    }
}