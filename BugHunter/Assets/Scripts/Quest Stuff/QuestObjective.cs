using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public int QuestStep = 0;
    private void Awake()
    {
        Invoke(nameof(Delay), 0.1f);   
    }
    void Delay()
    {
        QuestManager.instance.quests[QuestStep] = gameObject;
    }

}
