using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderMittens : MonoBehaviour
{
    private bool _IsAttacking = false;
    private GameObject Player;
    private int _Damage;
    private float StartRadius = 0;
    // Start is called before the first frame update
    private void Start()
    {
        StartRadius = gameObject.GetComponent<CapsuleCollider>().radius;
    }
    public void SetPlayer(GameObject gameObject)
    {
        Player = gameObject;
    }
    public void SetAttack(bool var, int Damage)
    {
        if (var==true)
        gameObject.GetComponent<CapsuleCollider>().radius = StartRadius* 3.0f;

        if (var == false)
            gameObject.GetComponent<CapsuleCollider>().radius = StartRadius;

        _IsAttacking = var;
        _Damage = Damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_IsAttacking == true&&collision.gameObject.tag=="Player")
        {
            Player.GetComponent<HealthSystem>().ModifyHealth(_Damage);
            Debug.Log("Hit");
        }
    }

}
