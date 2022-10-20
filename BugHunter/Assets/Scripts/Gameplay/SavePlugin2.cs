using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.UI;

public class SavePlugin2 : MonoBehaviour
{
    [DllImport("Plugin")]
    private static extern void SaveToFile(int id, float x, float y, float z, float Health);

    [DllImport("Plugin")]
    private static extern void StartWriting(string fileName);

    [DllImport("Plugin")]
    private static extern void EndWriting();

    [DllImport("Plugin")]
    private static extern float LoadFromFile(int j, string fileName);

    [DllImport("Plugin")]
    private static extern int GetLines(string fileName);

    [DllImport("DeviceTime")]
    private static extern int GenerateHour();

    [DllImport("DeviceTime")]
    private static extern int GenerateMin();

    [DllImport("DeviceTime")]
    private static extern int GenerateSec();

    public GameObject player;
    public Text LastSaveTxt;
    string m_Path;
    string fn;
    string fn2;
    string infoString;
    Vector3 LoadedPlayerPos3;
    int lineIndex;
    int Hour;
    int Min;
    int Sec;
    float PlayerSavedHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";
        fn2 = Application.dataPath + "/save.txt";

        Debug.Log(fn);
        //Debug.Log(fn2);
        lineIndex = 0;
        // optional Keyboard inputs for saving loading & Updating Time
        PlayerInput.SavePlayer += SaveItems;
        PlayerInput.LoadPlayer += LoadItems;
        PlayerInput.GetTime += GetTime; 
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
            
            SaveToFile(1, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z, player.GetComponent<HealthSystem>().GetHealth());
        }
        EndWriting();
        //Update the last savepoint text
        GetTime();
    }

    //File Loading using DLL
   public void LoadItems()
   {
        int numLines = GetLines(fn2);
        int maxItems = numLines / 4;

        // Read Player Position
        LoadedPlayerPos3.x = LoadFromFile(0, fn);
        LoadedPlayerPos3.y = LoadFromFile(1, fn);
        LoadedPlayerPos3.z = LoadFromFile(2, fn);
        PlayerSavedHealth = LoadFromFile(3, fn);

        //convert from float to int
        int PSavedHealth_I = (int)PlayerSavedHealth;

        Debug.Log("Health Loaded to: " + PSavedHealth_I);
        player.transform.position = LoadedPlayerPos3;
        player.GetComponent<HealthSystem>().SetHealth(PSavedHealth_I);
   }

    // uses DeviceTime DLL
    public void GetTime()
    {
        Hour = GenerateHour();
        Min = GenerateMin();
        Sec = GenerateSec();
        LastSaveTxt.text = "Last Save: " + Hour + ":" + Min + ":" + Sec;

        //Debug.Log("Current Time: " + Hour + ":" + Min + ":" + Sec);
    }
}
