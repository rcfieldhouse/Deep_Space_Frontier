namespace Client {

    using KaymakNetwork;
    using UnityEngine;
    
    
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
    
    internal static class NetworkSend
    {
        public static void SendPing()
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ClientPackets.CPing);
            buffer.WriteString("Client Connected");
            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);
    
            buffer.Dispose();
        }
    
        public static void SendKeyInput(InputManager.Keys key)
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ClientPackets.CKeyInput);
            buffer.WriteInt32((int)key);
    
            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);
    
            buffer.Dispose();
        }
        public static void SendMoveData(Vector2 vector)
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ClientPackets.CMoveData);
            buffer.WriteSingle(vector.x);
            buffer.WriteSingle(vector.y);
    
            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);
    
            buffer.Dispose();
        }
        public static void SendLookData(Vector2 vector)
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ClientPackets.CLookData);
            buffer.WriteSingle(vector.x);
            buffer.WriteSingle(vector.y);
    
    
            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);
    
            buffer.Dispose();
        }
    
    }


}