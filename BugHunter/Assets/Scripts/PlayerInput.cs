using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float LRMove = 0f;
    [SerializeField] private float FBMove = 0f;
    [SerializeField] private bool Jump = false; 
    [SerializeField] private Vector3 Direction;
    [SerializeField] private CharacterController controller; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Jump = true; 
        }
            LRMove = Input.GetAxisRaw("Horizontal");
            FBMove = Input.GetAxisRaw("Vertical");

        controller.Move(LRMove, FBMove, Jump);
    }
}
