using System.Threading;
using UnityEngine;

public class NetworkDriver : MonoBehaviour
{
    public bool isServer = false;
    private static Thread threadConsole;
    public string ip;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (isServer)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();

            ServerNetworkConfig.InitNetwork();
            ServerNetworkConfig.socket.StartListening(8888, 5, 1);
            Debug.Log("-------------------------===============Network Initialized===============-------------------------");
            Debug.Log("Host IP: " + System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1]);
        }
        else
        {
            ClientNetworkConfig.InitNetwork();
            ClientNetworkConfig.ConnectToServer(ip);
        }
    }


    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
        ClientNetworkConfig.DisconectFromServer();
    }

    private static void ConsoleThread()
    {
        while (true)
        {


            Thread.Sleep(30);
        }
    }
}
