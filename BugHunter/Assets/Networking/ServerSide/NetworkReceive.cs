using System;
using System.Collections.Generic;
using System.Text;
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

#region NetworkCommands
//Disgusting that I have to do this for thread safety.
public struct MovementCommand
{
    public MovementCommand(Vector2 vec, int ID)
    {
        vector = vec;
        connectionID = ID;
    }
    public Vector2 vector;
    public int connectionID;
}

public struct LookCommand
{
    public LookCommand(Vector2 lookVec, int ID)
    {
        vector = lookVec;
        connectionID = ID;
    }
    public Vector2 vector;
    public int connectionID;
}
#endregion

internal class NetworkReceive
{

    internal static void PacketRouter()
    {
        NetworkConfig.socket.PacketId[(int)ClientPackets.CPing] = Packet_SpawnPlayer;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CKeyInput] = Packet_KeyInput;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CPlayerRotation] = Packet_PlayerRotation;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CMessage] = Packet_Message;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CAnimation] = Packet_Animate;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CMoveData] = Packet_MoveData;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CLookData] = Packet_LookData;
    }

    private static void Packet_MoveData(int connectionID, ref byte[] data)
    {
        
        ByteBuffer buffer = new ByteBuffer(data);
        float moveX = buffer.ReadSingle();
        float moveY = buffer.ReadSingle();

        MovementCommand cmd = new MovementCommand(new Vector2(moveX, moveY), connectionID);

        GameManager._movementQueue.Enqueue(cmd);

        buffer.Dispose();
    }

    private static void Packet_LookData(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        float moveX = buffer.ReadSingle();
        float moveY = buffer.ReadSingle();

        if ((moveX > 30f) || (moveY > 30.0f))
            return;

        LookCommand cmd = new LookCommand(new Vector2(moveX, moveY), connectionID);

        GameManager._lookQueue.Enqueue(cmd);

  
        buffer.Dispose();

    }

    private static void Packet_Animate(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        //AnimationState anim = (AnimationState)buffer.ReadInt32();
        
        buffer.Dispose();
    }

    private static void Packet_SpawnPlayer(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        string msg = buffer.ReadString();

        Console.WriteLine(msg);
        GameManager.instance.CreatePlayer(connectionID);
        buffer.Dispose();
    }
    private static void Packet_KeyInput(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        byte key = buffer.ReadByte();

        buffer.Dispose();
        InputManager.HandleKeyInput(connectionID, (InputManager.Keys)key);

    }
    private static void Packet_PlayerRotation(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        float rotX = buffer.ReadSingle();
        float rotY = buffer.ReadSingle();
        float rotZ = buffer.ReadSingle();
        float rotW = buffer.ReadSingle();

        buffer.Dispose();

        

        GameManager.playerList[connectionID].transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);
    }

    private static void Packet_Message(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        string message = buffer.ReadString();

        buffer.Dispose();

        NetworkSend.SendMessage(connectionID, message);
    }
}

