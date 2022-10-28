using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassCreator : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        CreateClass(gameObject);
    }

    // Update is called once per frame
    public void CreateClass(GameObject Player)
    {
        ClassInterface ClassCreator = ClassFactory.SpawnClass(ClassType.Assault);
        ClassCreator.CreateClass(Player);
    }
}
