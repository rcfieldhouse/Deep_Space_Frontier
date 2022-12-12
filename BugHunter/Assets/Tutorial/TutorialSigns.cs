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

    public void SetStep(TutorialStep step)
    {
        if (Tutorials!=null)
        Tutorials[(int)step].SetActive(true);
        GameManager.instance.StopTime();
    }



    public void DisableSigns()
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
             if (Tutorials[i].activeInHierarchy == true) 
             StartCoroutine(wait());


            Tutorials[i].SetActive(false);
        }
        GameManager.instance.ResumeTime();
    }
    private IEnumerator wait(){

        yield return new WaitForEndOfFrame();
           GameObject.Find("UI Manager").GetComponent<UIManager>().ResumeGame();
    }
}
