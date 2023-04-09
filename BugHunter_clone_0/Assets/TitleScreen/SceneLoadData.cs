using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadData : MonoBehaviour
{
    ClassType ClassSelection;
   // public GameData data;
    bool AppStarting = true;
    // Start is called before the first frame update
    void Awake()
    {       
        DontDestroyOnLoad(gameObject);
        

       
    }
    private void Start()
    {
        Invoke(nameof(wait), 2.0f);
    }
    void wait()
    {
        AppStarting = false;
    }
    public void SetClass(ClassType ClassSelect)
    {
        if(AppStarting==false)
        ClassSelection = ClassSelect;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    public ClassType GetClass()
    {
        return ClassSelection;
    }

}
