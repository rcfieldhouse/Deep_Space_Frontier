using System.Net;
using System.Threading;
using UnityEngine;

public class Program : MonoBehaviour
{
	private static Thread threadConsole;

	void Start()
	{
		threadConsole = new Thread(new ThreadStart(ConsoleThread));
		threadConsole.Start();

		NetworkConfig.InitNetwork();
		NetworkConfig.socket.StartListening(8888, 5, 1);
		Debug.Log("Network Has Been Initialized at the IP: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList[1]);
	}

	private static void ConsoleThread()
	{
		while (true)
		{

		}
	}
}
