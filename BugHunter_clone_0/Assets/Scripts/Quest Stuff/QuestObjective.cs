using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class QuestObjective : NetworkBehaviour
{
    public int QuestStep = 0;
    public HealthSystem Health;

    public override void OnNetworkSpawn()
    {
        Invoke(nameof(Delay), 0.01f);
        Health = GetComponent<HealthSystem>();
        Health.OnObjectDeathT += HandleObjectDeath;
        base.OnNetworkSpawn();
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
