using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSigns : MonoBehaviour
{
    public static TutorialSigns instance;
    public List<GameObject> Tutorials;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerInput.PausePlugin += DisableSigns;
        if (instance == null) instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            Tutorials.Add(transform.GetChild(i).gameObject);
        }
    }
    private void OnDestroy()
    {
        PlayerInput.PausePlugin -= DisableSigns;
    }

    public void SetStep(TutorialStep step)
    {
        if (Tutorials != null)
            Tutorials[(int)step].SetActive(true);
        GameManager.instance.StopTime();
    }



    public void DisableSigns()
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].activeInHierarchy == true)
                Invoke(nameof(wait), 0.1f);
            Tutorials[i].SetActive(false);
        }
        GameManager.instance.ResumeTime();
    }
    private void wait(){

         transform.parent.parent.GetComponentInChildren<UIManager>().ResumeGame();
    }
}
