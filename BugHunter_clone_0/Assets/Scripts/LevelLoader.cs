using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        GameManager.instance.SceneChange(level);
    }
    public void LoadLevelWCharacter(string level)
    {
        ClassType ClassSelect;
        ClassSelect = transform.parent.parent.GetChild(0).GetComponent<ClassCreator>().GetClass();
        GameObject.Find("SceneLoadData").GetComponent<SceneLoadData>().SetClass(ClassSelect);
        GameManager.instance.SceneChange(level);
    }
}
