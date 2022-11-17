using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassCreator : MonoBehaviour
{
    [SerializeField] ClassType ClassSelection;
    // Start is called before the first frame update
    private void Start()
    {
        ClassSelection= GameObject.Find("SceneLoadData").GetComponent<SceneLoadData>().GetClass();
        CreateClass(gameObject);
    }
  
    // Update is called once per frame
    public void CreateClass(GameObject Player)
    {
        ClassInterface ClassCreator = ClassFactory.SpawnClass(ClassSelection);
        ClassCreator.CreateClass(Player);
    }
}
