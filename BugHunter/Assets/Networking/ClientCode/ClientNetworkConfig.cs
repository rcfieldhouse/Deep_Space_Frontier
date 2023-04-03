using UnityEngine;
using KaymakNetwork.Network;

enum ClientPackets
{
    CPing = 1,
    CKeyInput,
    CMessage,
    CPlayerData,
}

enum ServerPackets
{
    SWelcomeMsg = 1,
    SInstantiatePlayer,
    SMessage,
    SPlayerData,
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
        Debug.Log("Called");
        socket.Connect(ip, 8888);
    }

    public static void DisconectFromServer()
    {
        socket.Dispose();
    }
}
