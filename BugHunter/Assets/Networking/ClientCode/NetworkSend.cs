using KaymakNetwork;
using UnityEngine;

internal static class ClientNetworkSend
{
    public static void SendPing()
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CPing);
        buffer.WriteString("Client Connected");
        ClientNetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    public static void SendKeyInput(ClientInputManager.Keys key)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CKeyInput);
        buffer.WriteInt32((int)key);

        ClientNetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
    public static void SendMoveData(Vector2 vector)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CMoveData);
        buffer.WriteSingle(vector.x);
        buffer.WriteSingle(vector.y);

        ClientNetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
    public static void SendLookData(Vector2 vector)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CLookData);
        buffer.WriteSingle(vector.x);
        buffer.WriteSingle(vector.y);


        ClientNetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

}


