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
    private static extern float LoadFromFile(int j, string fileName);

    [DllImport("Plugin")]
    private static extern int GetLines(string fileName);

    public GameObject player;
    string m_Path;
    string fn;
    string fn2;
    string infoString;
    Vector3 LoadedPlayerPos;
    Vector3 LoadedPlayerPos2;
    Vector3 LoadedPlayerPos3;
    int lineIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";

        fn2 = Application.dataPath + "/save.txt";
        Debug.Log(fn);
        Debug.Log(fn2);
        lineIndex = 0;
        PlayerInput.SavePlayer += SaveItems;
        // PlayerInput.LoadPlayer += ReadItems;
        //PlayerInput.LoadPlayer += LoadRequest;
        PlayerInput.LoadPlayer += LoadItems;


    }

   public void SaveItems()
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

    // File Loading using C#
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

    //File Loading using DLL
   public void LoadItems()
    {
        int numLines = GetLines(fn2);
        int maxItems = numLines / 3;
        int infoSet = 0;

        // Read Player Position
        LoadedPlayerPos3.x = LoadFromFile(0, fn);
        LoadedPlayerPos3.y = LoadFromFile(1, fn);
        LoadedPlayerPos3.z = LoadFromFile(2, fn);
        Debug.Log(LoadedPlayerPos3);
        player.transform.position = LoadedPlayerPos3;
    }
}
