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

        buffer.WriteInt32(ClientNetworkManager.instance.myConnectionID);

        GameObject player = ClientNetworkManager.playerList[ClientNetworkManager.instance.myConnectionID];

        //Position
        buffer.WriteSingle(player.transform.position.x);
        buffer.WriteSingle(player.transform.position.y);
        buffer.WriteSingle(player.transform.position.z);
        Debug.Log("Position Sent is: " + player.transform.position);

        //rotation
        buffer.WriteSingle(player.transform.rotation.x);
        buffer.WriteSingle(player.transform.rotation.y);
        buffer.WriteSingle(player.transform.rotation.z);
        buffer.WriteSingle(player.transform.rotation.w);
        Debug.Log("Rotation Sent is: " + player.transform.rotation);

        //velocity
        buffer.WriteSingle(player.GetComponentInChildren<Rigidbody>().velocity.x);
        buffer.WriteSingle(player.GetComponentInChildren<Rigidbody>().velocity.y);
        buffer.WriteSingle(player.GetComponentInChildren<Rigidbody>().velocity.z);
        Debug.Log("Velocity Sent is: " + player.GetComponentInChildren<Rigidbody>().velocity);

        //Health
        buffer.WriteInt32(player.GetComponentInChildren<HealthSystem>().GetHealth());
        Debug.Log("Health is: " + player.GetComponentInChildren<HealthSystem>().GetHealth());

        ClientNetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    public static void SendKeyInput(PlayerStates action)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CKeyInput);
        buffer.WriteInt32((int)action);

        Debug.Log("Sending Key Input: "+action);

        ClientNetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
}


