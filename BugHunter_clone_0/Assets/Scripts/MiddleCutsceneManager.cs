using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MiddleCutsceneManager : MonoBehaviour
{
    public GameObject FirstCutsceneParent;

    public GameObject BrokenTreeBarrier;
    public GameObject PlaceholderTree;

    public GameObject PlayerCutscenePos;

    public PlayableDirector director;
    public GameObject CutsceneAnimTree;
    private bool PlayerInCutscene = false;
    private bool MCutsceneEnded = false;

    //public GameObject Player1;
    //public GameObject Player2;
    //public GameObject Player3;

    // Start is called before the first frame update
    void Start()
    {
        MCutsceneEnded = false;
    }

    private void Update()
    {
        if (director.state != PlayState.Playing && PlayerInCutscene == true)
        {
            if(MCutsceneEnded == false)
            {
                CutsceneEnded();
                MCutsceneEnded = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FirstCutsceneParent.SetActive(true);
            PlayerInCutscene = true;
            PlaceholderTree.SetActive(false);
            other.gameObject.transform.position = PlayerCutscenePos.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }

    private void CutsceneEnded()
    {
        BrokenTreeBarrier.SetActive(true);
        CutsceneAnimTree.SetActive(false);
        FirstCutsceneParent.SetActive(false);
    }
}
