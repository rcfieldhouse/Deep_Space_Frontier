using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ClientInputManager : MonoBehaviour
{
    
    public Keys pressedKey;
    public enum Keys
    {
        None,
        W,
        A,
        S,
        D,
        Mouse1,
        Mouse2,
        MouseScroll,
    }


    // Use this for initialization
    void Start()
    {
        
        pressedKey = Keys.None;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       
    }


}

