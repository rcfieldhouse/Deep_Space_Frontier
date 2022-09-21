using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAi : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider CC;
    public GameObject player; 
    private Vector3 distVec;
    private float DistanceToPlayer;
    [SerializeField] float speed;
    void Start()
    {
        player = gameObject.GetComponentInParent<GruntManager>().Player;
        speed = 5.0f;
        rb=GetComponent<Rigidbody>();
        CC=GetComponent<CapsuleCollider>(); 
    }

    // Update is called once per frame
    void Update()
    {
 
        distVec=(player.transform.position - rb.gameObject.transform.position);
        DistanceToPlayer = Mathf.Abs(distVec.x) + Mathf.Abs(distVec.y) + Mathf.Abs(distVec.z);
        //Debug.Log(Mathf.Abs(distVec.x) + Mathf.Abs(distVec.y) + Mathf.Abs(distVec.z));
       
        Seek();
    }
    public void Chase()
    {
        rb.velocity = (distVec.normalized*speed);
        //this one will chase indefinetly 
        //used in seek but under a condition
        //aka ape shit mode
    }
    public void Seek()
    {
        //this one is basic distance calc
        if (DistanceToPlayer < 10.0f)
        {
         
            transform.LookAt(player.transform);
            Chase();
            Debug.Log("holy shit it works");
        }
    }
    public void IdleMove()
    {
        //i have no idea how to do this rn, will do later cause lazy
    }
    public void Attack()
    {

    }
}
