using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum QuestStep
{
    GetFruit, DestroyBarrier, KillEnemies
}
public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<string> Quests;
    private int QuestNum = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Quests.Add("Pickup Fruit");
        Quests.Add("Destroy Barrier");
        Quests.Add("Kill The Enemies");
        GetComponentInChildren<TextMeshProUGUI>().text = "Quest: " + Quests[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateQuest(QuestStep QuestSelect)
    {
       switch (QuestSelect)
        {
            case QuestStep.GetFruit:
                QuestNum = 0; 
                break;
            case QuestStep.DestroyBarrier:
                QuestNum = 1;
                break;
            case QuestStep.KillEnemies:
                QuestNum = 2;
                break;
        }
        //display + 1 so that you store ur current completed quest step but you also show your next one

        GetComponentInChildren<TextMeshProUGUI>().text = "Quest: " + Quests[QuestNum+1];
    }

  //  GetComponentInChildren<TextMeshProUGUI>().text="Quest: "+ Quests[0];
}
