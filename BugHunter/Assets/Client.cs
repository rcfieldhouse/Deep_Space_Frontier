using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lec 04
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
    public GameObject Cube;
    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket clientSocket;

    public static void StartClient()
    {
        try {
            IPAddress ip = IPAddress.Parse("10.150.7.240");
            remoteEP = new IPEndPoint(ip, 8888);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.Find("Cube");
        StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        outBuffer = Encoding.ASCII.GetBytes(Cube.transform.position.x.ToString());
        clientSocket.SendTo(outBuffer,remoteEP);
    }
}
