using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenCutscene : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public void descend()
    {
        GetComponent<Animator>().Play("DeathFinal");
    }
    // Start is called before the first frame update
    void Awake()
    {


    }


    // Update is called once per frame
    void Update()
    {
        descend();
        //GetComponent<Rigidbody>().velocity = Vector3.down * moveSpeed;
    }
}