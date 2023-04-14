using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public int CurrentQuestStep=0;
    public GameObject Marker,MarkerInstance;
    public List<GameObject> quests;
    private GameObject Player;
    [Range(1, 5)] public float SizeMarker = 1;
    private void Awake()
    {
        Player = GameObject.Find("MixamoCharacter");
        if (instance == null)
            instance = this;

       MarkerInstance= Instantiate(Marker);

        Invoke(nameof(SetInitQuest), 0.05f);
    }
    public void SetNewQuest(int index)
    {
        CurrentQuestStep = index+1;
        float ExtraHeight;
        if (quests[0].GetComponent<MeshRenderer>() != null)
            ExtraHeight = quests[CurrentQuestStep].GetComponent<MeshRenderer>().bounds.size.y;
        else ExtraHeight = quests[CurrentQuestStep].GetComponentInChildren<MeshRenderer>().bounds.size.y;
        MarkerInstance.transform.position = quests[CurrentQuestStep].transform.position + (Vector3.up * ExtraHeight) + Vector3.up;
    }
    public void SetInitQuest()
    {
        float ExtraHeight;
        if (quests[0].GetComponent<MeshRenderer>()!=null)
         ExtraHeight = quests[0].GetComponent<MeshRenderer>().bounds.size.y;
        else  ExtraHeight = quests[0].GetComponentInChildren<MeshRenderer>().bounds.size.y;
        MarkerInstance.transform.position = quests[0].transform.position + (Vector3.up * ExtraHeight) + Vector3.up;
    }
    private void Update()
    {
        if ((Player.transform.position - MarkerInstance.transform.position).magnitude > 100.0f)
            MarkerInstance.transform.GetChild(0).localScale = Vector3.one * (Player.transform.position - MarkerInstance.transform.position).magnitude/100.0f * SizeMarker;
    }
}
