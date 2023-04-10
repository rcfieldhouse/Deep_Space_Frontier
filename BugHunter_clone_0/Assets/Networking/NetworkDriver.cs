using System.Net;
using System.Threading;
using UnityEngine;

public class NetworkDriver : MonoBehaviour
{
    private static NetworkDriver _instance;
    public static NetworkDriver instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("NetworkDriver instance is null!");
            return _instance;
        }
    }

    public bool isServer = false;
    private static Thread threadConsole;
    public string serverIp;
    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        IPAddress ipAddress = ipHostInfo.AddressList[1];
        if (isServer)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();

            ServerNetworkConfig.InitNetwork();
            ServerNetworkConfig.socket.StartListening(8888, 5, 1);
            Debug.Log("-------------------------===============Network Initialized===============-------------------------");
            Debug.Log("Host IP: " + ipAddress);
        }
        else
        {
            ClientNetworkConfig.InitNetwork();
            ClientNetworkConfig.ConnectToServer(serverIp);
        }
    }


    private void OnApplicationQuit()
    {
        if (!isServer)
        {
            ClientNetworkConfig.DisconectFromServer();
            Debug.Log("Quit");
        }

    }

    private static void ConsoleThread()
    {
        while (true)
        {


            Thread.Sleep(30);
        }
    }
}