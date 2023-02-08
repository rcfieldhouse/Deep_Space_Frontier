using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public int CurrentQuestStep=0;
    public GameObject Marker,MarkerInstance;
    public List<GameObject> quests;

    private void Awake()
    {
        if (instance == null)
            instance = this;

       MarkerInstance= Instantiate(Marker);
        Invoke(nameof(SetInitQuest), 0.05f);
    }
    public void SetNewQuest(int index)
    {
        CurrentQuestStep = index+1;
        float ExtraHeight = quests[CurrentQuestStep].GetComponent<MeshRenderer>().bounds.size.y;
        MarkerInstance.transform.position = quests[CurrentQuestStep].transform.position + (Vector3.up * ExtraHeight) + Vector3.up;
    }
    public void SetInitQuest()
    {
        float ExtraHeight = quests[0].GetComponent<MeshRenderer>().bounds.size.y;
        MarkerInstance.transform.position = quests[0].transform.position + (Vector3.up * ExtraHeight) + Vector3.up;
    }
    
}
