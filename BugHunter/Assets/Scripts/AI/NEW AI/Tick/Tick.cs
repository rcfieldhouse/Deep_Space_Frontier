using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Tick : AI
{
    [Range(0,5)] public float DamageTime = 5.0f, DamageInterval = 0.25f;
    [Range(0,-10)]public int Damage = -5;  
    public override void AttackPlayer(GameObject Target)
    {
        //play Dante.sound.ogg tick attack
        Debug.Log("Attack"+Target.name);
        if (Target.GetComponent<Venom>() == null)
            Target.AddComponent<Venom>().InitAttack(DamageTime,DamageInterval,Damage);
    }
    
   


}
