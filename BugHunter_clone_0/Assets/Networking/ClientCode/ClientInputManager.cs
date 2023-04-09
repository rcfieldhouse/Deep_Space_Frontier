using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class ClientInputManager : MonoBehaviour
{
    Inputs PlayerInputController;


    private void Start()
    {
        //InputSystem.onAnyButtonPress.Call(SendInputData);
    }

    public void FixedUpdate()
    {
        //SendInputData();
    }

    private void SendInputData(InputControl inputControl)
    {
        ClientNetworkSend.SendPlayerData();
    }
    private void SendInputData()
    {
        ClientNetworkSend.SendPlayerData();
    }

}

