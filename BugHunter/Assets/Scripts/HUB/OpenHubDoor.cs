using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class OpenHubDoor : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something Entered trigger");
        if(other.tag == "Player")
        {
            animator.SetBool("isOpen", true);
            Debug.Log("Player entered Trigger Animation should play now");
        }
    }
}
