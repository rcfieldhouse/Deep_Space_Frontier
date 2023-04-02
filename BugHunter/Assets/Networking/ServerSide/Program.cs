using System;
using System.Threading;
using UnityEngine;


class Program : MonoBehaviour
{
    private static Thread threadConsole;

    public void Start()
    {
        threadConsole = new Thread(new ThreadStart(ConsoleThread));
        threadConsole.Start();

        NetworkConfig.InitNetwork();
        NetworkConfig.socket.StartListening(8888, 5, 1);
        Debug.Log("-------------------------===============Network Initialized===============-------------------------");
        Debug.Log("Host IP: "+ System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1]);
    }

    private static void ConsoleThread()
    {
        while(true)
        {


            Thread.Sleep(30);
        }
    }
}

