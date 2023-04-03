using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaymakNetwork.Network;

internal static class ClientNetworkConfig
{
    internal static Client socket;

    internal static void InitNetwork()
    {
        if (!ReferenceEquals(socket, null)) return;
        socket = new Client(100);
        ClientNetworkReceive.PacketRouter();
    }

    internal static void ConnectToServer(string ip)
    {
        socket.Connect(ip, 8888);
    }

    internal static void DisconectFromServer()
    {
        socket.Dispose();
    }
}
