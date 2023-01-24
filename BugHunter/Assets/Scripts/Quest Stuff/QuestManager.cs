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
    //quest num is for rendering right string on the UI 
    //CurrentQuestNum is for the manager to know what quest the player is on 
    //Quest Progression is what kinda quest it is (see enum above for quest types)
    //Quests is the list of strings for UI
    public static QuestManager instance;
    public List<string> Quests;
    public List<QuestStep> QuestProgression;
    private int QuestNum = 0,CurrentQuestNum=0;
 
    // Start is called before the first frame update
    void Awake()
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

   
 
    public void QuestCompleted(QuestStep QuestSelect)
    {
        if (QuestSelect == QuestProgression[CurrentQuestNum])
        {
            CurrentQuestNum++;
            UpdateQuest(QuestProgression[CurrentQuestNum]);
        }
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

        GetComponentInChildren<TextMeshProUGUI>().text = "Quest: " + Quests[QuestNum];
    }

    public int GetCurrentQuestNum()
    {
        return CurrentQuestNum;
    }

  //  GetComponentInChildren<TextMeshProUGUI>().text="Quest: "+ Quests[0];
}
