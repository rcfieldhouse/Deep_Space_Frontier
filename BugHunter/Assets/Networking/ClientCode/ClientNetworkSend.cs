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

    public static void SendPlayerData()
    {
        Debug.Log("SendPlayerData Triggered");
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CPlayerData);

        //ID to send to
        buffer.WriteInt32(ClientNetworkManager.instance.myConnectionID);
        //Owner of Data
        //might not need
        buffer.WriteInt32(ClientNetworkManager.instance.myConnectionID);

        GameObject pData = ClientNetworkManager.playerList[ClientNetworkManager.instance.myConnectionID];
        //position
        buffer.WriteSingle(pData.transform.position.x);
        buffer.WriteSingle(pData.transform.position.y);
        buffer.WriteSingle(pData.transform.position.z);

        //rotation
        buffer.WriteSingle(pData.transform.rotation.eulerAngles.x);
        buffer.WriteSingle(pData.transform.rotation.eulerAngles.y);
        buffer.WriteSingle(pData.transform.rotation.eulerAngles.z);

        //velocity
        buffer.WriteSingle(pData.GetComponentInChildren<Rigidbody>().velocity.x);
        buffer.WriteSingle(pData.GetComponentInChildren<Rigidbody>().velocity.y);
        buffer.WriteSingle(pData.GetComponentInChildren<Rigidbody>().velocity.z);

        //Animations
        //buffer.WriteInt32((int)ClientInputManager.currentAction);
        //
        ////bools
        //buffer.WriteBoolean(Data.isDead);
        //buffer.WriteBoolean(Data.isFiring);
        //
        ////Health
        //buffer.WriteSingle(Data.HealthAmount);

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
}


