using System;
using System.Collections.Generic;
using KaymakNetwork;

public enum ClientPackets
{
    CPing = 1,
    CKeyInput,
    CPlayerRotation,
    CMessage,
    CGrenadeThrow,
}
internal static class NetworkSend
{
    private static Queue<INetwork> commands;

    public static void DoSerialize()
    {
        for (int i = 0; i<commands.Count; i++)
            if (commands != null)
            {
                INetwork command = commands.Dequeue();
            }
    }

    public static void SendPing()
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CPing);
        buffer.WriteString("Client Connected");
        NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
    public static void SendMessage(string message)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CMessage);
        buffer.WriteString(message);

        NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
    public static void SendKeyInput(InputManager.Keys pressedKey)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CKeyInput);
        buffer.WriteByte((byte)pressedKey);
        NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    internal static void SendPlayerRotation(float rotation)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CPlayerRotation);
        buffer.WriteSingle(rotation);
        NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
}

