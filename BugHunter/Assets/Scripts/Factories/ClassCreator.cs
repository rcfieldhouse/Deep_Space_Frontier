using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassCreator : MonoBehaviour
{
    [SerializeField] ClassType ClassSelection;
    // Start is called before the first frame update
    private void Awake()
    {
        
        //CreateAClass();
       Invoke(nameof(CreateAClass), 0.05f);
    }
    public void CreateAClass()
    {
        if (GameObject.Find("SceneLoadData") != null)
        {
            ClassSelection = GameObject.Find("SceneLoadData").GetComponent<SceneLoadData>().GetClass();
        }
        CreateClass(gameObject);
        if (GameObject.Find("PickupPrompt")!=null)
        GameObject.Find("PickupPrompt").SetActive(false);
    }
    // Update is called once per frame
    public void CreateClass(GameObject Player)
    {
        ClassInterface ClassCreator = ClassFactory.SpawnClass(ClassSelection);
        ClassCreator.CreateClass(Player);
    }
}
