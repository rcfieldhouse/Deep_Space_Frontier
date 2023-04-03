using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ServerInputManager : MonoBehaviour
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
    }

    public static void HandleKeyInput(int connectionID, Keys key)
    {
        if (key == Keys.None) return;

        //Send New Velocity
        ServerNetworkSend.SendPlayerMove(connectionID, ServerNetworkManager.playerList[connectionID].Position);
    }
}

