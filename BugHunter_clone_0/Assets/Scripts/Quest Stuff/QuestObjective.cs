using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public int QuestStep = 0;
    public HealthSystem Health;
    private void Awake()
    {
       
        Invoke(nameof(Delay), 0.01f);
        Health = GetComponent<HealthSystem>();
        Health.OnObjectDeathT += HandleObjectDeath;
    }
    private void OnDisable()
    {
        Health.OnObjectDeathT -= HandleObjectDeath;
    }
    void Delay()
    {
        QuestManager.instance.quests[QuestStep] = gameObject;
    }
    public void HandleObjectDeath(Transform  context)
    {
        QuestManager.instance.SetNewQuest(QuestStep);
    }
}
