using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public static QuestObjective instance;
    [SerializeField] QuestStep ThisQuestStep;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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
