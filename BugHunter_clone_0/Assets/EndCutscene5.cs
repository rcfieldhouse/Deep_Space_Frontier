using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene5 : MonoBehaviour
{
    public GameObject Cutscene5;
    // Update is called once per frame
    private void Awake()
    {
        if (Cutscene5 == null)
            Cutscene5 = GameObject.Find("CutsceneParent5");
        Debug.Log("IT HAPPENED1");

    }
    public void StartCutscene()
    {
        StartCoroutine(Cutscene());
    }
    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(30);
        Debug.Log("IT HAPPENED2");
        
        Cutscene5.SetActive(true);
        GameObject.Find("StupidQueen").SetActive(false);
        //transform.parent.gameObject.SetActive(false);
    }
}
