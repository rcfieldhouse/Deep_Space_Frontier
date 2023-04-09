using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBomb : MonoBehaviour
{
    public LayerMask WhatIsGround;
    public GameObject VFX;
    private void OnCollisionEnter(Collision collision)
    {
        SpawnGrenadeVFX();
        Destroy(gameObject);
    }
    public void SpawnGrenadeVFX()
    {
        if (VFX != null)
        {
            Instantiate(VFX, transform.position, Quaternion.identity);
        }

    }

}
