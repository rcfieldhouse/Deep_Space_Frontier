using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public int CurrentQuestStep=0;
    public GameObject Marker;
    public List<GameObject> quests;
    private void Awake()
    {
        if (instance == null)
            instance = this;

    Instantiate(Marker);
        Invoke(nameof(SetNewQuest), 0.5f);
    }
  void SetNewQuest()
    {
        Marker.transform.position = (quests[CurrentQuestStep]).transform.position + Vector3.up * 3;
    }
    // Update is called once per frame
}
