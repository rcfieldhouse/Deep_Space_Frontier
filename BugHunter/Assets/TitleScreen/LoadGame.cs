using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadDeepSpaceFrontier()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }
}