using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.UI;

public class SavePlugin2 : MonoBehaviour
{
    [DllImport("Plugin")]
    private static extern void SaveToFile(int id, float x, float y, float z, float Health, float Accuracy, float MagAmmo, float ReserveAmmo, int GrenadeAmount);

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

    public static SavePlugin2 instance;
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
    float PlayerAccuracy;
    float PlayerCurrentQuest;
    float PlayerGrenadeAmount;
    float PlayerMagAmmo;
    float PlayerReserveAmmo;
    public GameObject rifle;
    WeaponInfo weaponInfo;

    // Start is called before the first frame update
    void Start()
    {
        weaponInfo = rifle.GetComponent<WeaponInfo>();
        
        if (instance == null)
        {
            instance = this;
        }

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

        // Save to File ID & Player X, Y, Z position & PlayerHealth & Player Accuracy & Current Objective & Current Grenade Amount
        SaveToFile(1, player.transform.position.x, player.transform.position.y, player.transform.position.z, player.GetComponent<HealthSystem>().GetHealth(),
            StatisticTracker.instance.GetAccuracy(), weaponInfo.GetMag(), weaponInfo.GetReserveAmmo(), GrenadeManager.instance.GetNumNades());

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
        // Read Player Saved Stats
        PlayerSavedHealth = LoadFromFile(3, fn);
        PlayerAccuracy = LoadFromFile(4, fn);
        PlayerMagAmmo = LoadFromFile(5, fn);
        PlayerReserveAmmo = LoadFromFile(6, fn);
        PlayerGrenadeAmount = LoadFromFile(7, fn);


        //convert from float to int
        int PSavedHealth_I = (int)PlayerSavedHealth;
        int PCurrentQuest_I = (int)PlayerCurrentQuest;
        int PGrenadeAmount_I = (int)PlayerGrenadeAmount;
        int PMagAmmo_I = (int)PlayerMagAmmo;
        int PReserveAmmo = (int)PlayerReserveAmmo;


        // Used Loaded Stats to Reset Player
        Debug.Log("Health Loaded to: " + PSavedHealth_I);
        Debug.Log("Accuracy Was: " + PlayerAccuracy);

        // set position
        player.transform.position = LoadedPlayerPos3;
        // set health
        player.GetComponent<HealthSystem>().SetHealth(PSavedHealth_I);
        // set grenade amount
        GrenadeManager.instance.SetGrenades(PGrenadeAmount_I);
        ThrowableSwap.instance.DisplayNum(0);
        // Set Ammo/Mag
        weaponInfo.SetMag(PMagAmmo_I);
        weaponInfo.SetReserveAmmo(PReserveAmmo);

        // set quest
        // QuestManager.instance.UpdateQuest(QuestManager.instance.QuestProgression[PCurrentQuest_I]);
        //QuestObjective.instance.ResetPosition();
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
