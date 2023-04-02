using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InputManager : MonoBehaviour
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

        if (key == Keys.W)
        {
           // GameManager.playerList[connectionID].GetComponent<>();
        }
        else if (key == Keys.S)
        {

        }
        else if (key == Keys.A)
        {

        }
        else if (key == Keys.D)
        {

        }

        //Send New Velocity
        NetworkSend.SendPlayerMove(connectionID, GameManager.playerList[connectionID].transform.position);
    }
}

