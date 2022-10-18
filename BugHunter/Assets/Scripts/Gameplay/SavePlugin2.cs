using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

public class SavePlugin2 : MonoBehaviour
{
    [DllImport("Plugin")]
    private static extern int GetID();

    [DllImport("Plugin")]
    private static extern void SetID(int id);

    [DllImport("Plugin")]
    private static extern Vector3 GetPosition();

    [DllImport("Plugin")]
    private static extern void SetPosition(float x, float y, float z);

    [DllImport("Plugin")]
    private static extern void SaveToFile(int id, float x, float y, float z);

    [DllImport("Plugin")]
    private static extern void StartWriting(string fileName);

    [DllImport("Plugin")]
    private static extern void EndWriting();

    [DllImport("Plugin")]
    private static extern void ReadPlayerFilePos();

    [DllImport("Plugin")]
    private static extern void StartReading(string fileName);

    [DllImport("Plugin")]
    private static extern void EndReading();

    public GameObject player;
    string m_Path;
    string fn;
    string fn2;
    string fn3;
    string infoString;
    Vector3 LoadedPlayerPos;
    Vector3 LoadedPlayerPos2;
    int lineIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";
        fn2 = Application.streamingAssetsPath + "/PluginSaveFile/" + "save2" + ".txt";
        fn3 = "/save.txt";
        Debug.Log(fn);
        lineIndex = 0;
        PlayerInput.SavePlayer += SaveItems;
        // PlayerInput.LoadPlayer += ReadItems;
        PlayerInput.LoadPlayer += LoadRequest;


    }

    void SaveItems()
    {
        Debug.Log("Save Request Initiated");
        StartWriting(fn);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            /*if(obj.name.Contains("Player"))
            {
                SaveToFile(1, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            }*/
            SaveToFile(1, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
        }
        EndWriting();
    }

    void ReadItems()
    {
        StartReading(fn3);

        //LoadedPlayerPos = ReadPlayerFilePos();
        ReadPlayerFilePos();
         Debug.Log("PlayerPos is " + LoadedPlayerPos);
        //Debug.Log("Grab Player Pos");

        EndReading();
    }

    void LoadRequest()
    {
        Debug.Log("Load Request Initiated");
        LoadedPlayerPos2.x = float.Parse(ReadPlayerInfo(0));
        LoadedPlayerPos2.y = float.Parse(ReadPlayerInfo(1));
        LoadedPlayerPos2.z = float.Parse(ReadPlayerInfo(2));
        Debug.Log(LoadedPlayerPos2);
        player.transform.position = LoadedPlayerPos2;
    }

    string ReadPlayerInfo(int Index)
    {
        string[] lines = File.ReadAllLines(fn);

        return lines[Index];
    }
}
