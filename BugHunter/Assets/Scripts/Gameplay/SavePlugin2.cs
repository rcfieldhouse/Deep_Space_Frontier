using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

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

    string m_Path;
    string fn;

    // Start is called before the first frame update
    void Start()
    {
        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";
        Debug.Log(fn);
        PlayerInput.SavePlayer += SaveItems;

    }

    void SaveItems()
    {
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
}
