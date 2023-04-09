using System;
using UnityEngine;

public class EnemyNetworkState : MonoBehaviour
{
    public EnemyStates currentState;
    private Animator anim;
    public int myID;
    private void Start()
    {
        currentState = EnemyStates.None;
        anim = GetComponentInChildren<Animator>();
        //myID = 
    }

    public void UpdateState()
    {
        if (currentState == EnemyStates.None)
            return;
        else if (currentState == EnemyStates.Attacking)
            anim.Play("Attack");

    }

}