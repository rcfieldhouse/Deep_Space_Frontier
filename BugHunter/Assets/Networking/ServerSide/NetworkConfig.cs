using System;
using KaymakNetwork.Network;
using UnityEngine;

class NetworkConfig : MonoBehaviour
{
    private static Server _socket;
    internal static Server socket
    {
        get { return _socket;}
        set {
            if (_socket != null)
            {
                _socket.ConnectionReceived -= Socket_ConnectionReceived;
                _socket.ConnectionLost -= Socket_ConnectionLost;
            }

            _socket = value;
            if(_socket != null)
            {
                _socket.ConnectionReceived += Socket_ConnectionReceived;
                _socket.ConnectionLost += Socket_ConnectionLost;
            }
        }
    }

    internal static void InitNetwork()
    {
        if (!(socket == null))
            return;

        socket = new Server(100)
        {
            BufferLimit = 2048000,
            PacketAcceptLimit = 100,
            PacketDisconnectCount = 150
        };

        NetworkReceive.PacketRouter();
    }

    internal static void Socket_ConnectionReceived(int connectionID)
    {
        //Debug.Log(_socket.ClientIp(connectionID));
        Debug.Log("Connection received on index ["+connectionID+"]");

        NetworkSend.WelcomeMsg(connectionID, "You Are Connected to DeepSpace! Operative: '"+connectionID+"'");
    }

    internal static void Socket_ConnectionLost(int connectionID)
    {
        //Debug.Log(_socket.ClientIp(connectionID));
        Debug.Log("Connection lost on index ["+connectionID+"]");

    }
}

