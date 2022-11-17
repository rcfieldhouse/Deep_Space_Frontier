using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NewQuestManager : MonoBehaviour
{
    public static NewQuestManager _instance;
    private IQuest currentQuest;
    public TextMeshProUGUI UI;
    public UnityEvent CompleteQuest = new UnityEvent();

    private void Start()
    {
        CompleteQuest.AddListener(QuestIsComplete);
        //ChangeQuest()

        UI = GetComponentInChildren<TextMeshProUGUI>();

        if (_instance == null)
            _instance = new NewQuestManager();
    }

    void ChangeQuest(IQuest quest)
    {
        UI.text = "Quest: " + quest.MessageToDisplay;
        currentQuest = quest;
    }

    void QuestIsComplete()
    {
        currentQuest.isComplete = true;
        ChangeQuest(currentQuest.NextQuestInChain);

        //play sound
        //give reward
        //do stuff
    }

}
