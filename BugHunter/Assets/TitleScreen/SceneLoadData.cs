using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadData : MonoBehaviour
{
    ClassType ClassSelection;
    // Start is called before the first frame update
    void Awake()
    {       
        DontDestroyOnLoad(gameObject);
    }

    public void SetClass(ClassType ClassSelect)
    {
        ClassSelection = ClassSelect;
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene"|| SceneManager.GetActiveScene().name == "Hub")
        {
            MainSceneLoaded();
        }     
    }
    public ClassType GetClass()
    {
        return ClassSelection;
    }
    private void MainSceneLoaded()
    {
        GameObject.Find("MixamoCharacter").GetComponent<CharacterController>().disableCams(false);
    }
    
}
