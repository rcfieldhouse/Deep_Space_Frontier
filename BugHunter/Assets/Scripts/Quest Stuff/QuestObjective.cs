using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    [SerializeField] QuestStep ThisQuestStep;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private void OnDisable()
    {
        QuestManager.instance.QuestCompleted(ThisQuestStep);
    }

    public void ResetPosition()
    {
        gameObject.transform.position = startPos;
    }

}
