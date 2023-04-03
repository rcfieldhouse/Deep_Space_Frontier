using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadGame : MonoBehaviour
{
    [SerializeField] ClassType ClassSelect;
    // Start is called before the first frame update
    public void LoadDeepSpaceFrontier()
    {
        GameObject.Find("SceneLoadData").GetComponent<SceneLoadData>().SetClass(ClassSelect);
        //GameManager.instance.SceneChange("SampleScene");
        GameManager.instance.SceneChange("Hub");
        //SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }

    public void LoadDeepSpaceFrontierTutorial()
    {
        GameObject.Find("SceneLoadData").GetComponent<SceneLoadData>().SetClass(ClassSelect);
        //GameManager.instance.SceneChange("SampleScene");
        GameManager.instance.SceneChange("Hub 1");
        //SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }
}