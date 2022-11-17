using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IQuest : MonoBehaviour
{
    IQuest(string message)
    {
        MessageToDisplay = message;
        //add more functionality here
    }

    public string MessageToDisplay;
    public IQuest NextQuestInChain;
    public bool isComplete = false;

    public void ObjectiveComplete()
    {
        NewQuestManager._instance.CompleteQuest.Invoke();
    }

}
