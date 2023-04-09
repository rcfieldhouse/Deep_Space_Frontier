using UnityEngine;
using KaymakNetwork.Network;

public static class ClientNetworkConfig
{
    internal static Client socket;

    public static void InitNetwork()
    {
        if (!ReferenceEquals(socket, null)) return;
        socket = new Client(100);

        ClientNetworkReceive.PacketRouter();
    }

    public static void ConnectToServer(string ip)
    {
        socket.Connect(ip, 8888);
    }

    public static void DisconectFromServer()
    {
        socket.Dispose();
    }
}
