using System;
using KaymakNetwork.Network;


internal static class NetworkConfig
{
    internal static Client socket;
    internal static void InitNetwork()
    {
        if (!ReferenceEquals(socket, null)) return;
        socket = new Client(100);
        NetworkReceive.PacketRouter();
    }

    internal static void ConnectToServer()
    {
        socket.Connect("localhost", 8888);
    }

    internal static void DisconnectFromServer()
    {
        socket.Dispose();
    }
}

