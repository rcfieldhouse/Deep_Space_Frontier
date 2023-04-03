using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaymakNetwork.Network;

enum ClientPackets
{
    CPing = 1,
    CKeyInput,
    CPlayerRotation,
    CMessage,
    CAnimation,
    CMoveData,
    CLookData,
}


enum ServerPackets
{
    SWelcomeMsg = 1,
    SInstantiatePlayer,
    SPlayerMove,
    SPlayerRotation,
    SMessage,
    SAnimation,
}

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
