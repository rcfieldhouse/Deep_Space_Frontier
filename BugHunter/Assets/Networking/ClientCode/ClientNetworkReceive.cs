using KaymakNetwork;
using UnityEngine;

internal static class ClientNetworkReceive
{
    internal static void PacketRouter()
    {
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = Packet_WelcomeMsg;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = Packet_Spawn;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SMessage] = Packet_Message;
        ClientNetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerData] = Packet_PlayerData;

    }

    private static void Packet_WelcomeMsg(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        string msg = buffer.ReadString();
        buffer.Dispose();

        Debug.Log(msg);

        ClientNetworkSend.SendPing();
    }

    private static void Packet_Spawn(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        //Spawn OTHER players here

        int id = buffer.ReadInt32();
        ClientNetworkManager.instance._IDToSet.Enqueue(id);

        Debug.Log("Spawn");

        buffer.Dispose();
    }

    private static void Packet_KeyInput(ref byte[] data)
    {
        Debug.Log("Key Input");
    }

    private static void Packet_Message(ref byte[] data)
    {
        Debug.Log("Message");
    }
}


