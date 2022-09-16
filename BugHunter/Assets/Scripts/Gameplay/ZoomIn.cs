using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private bool isScoped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
        }
    }
}
