using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadData : MonoBehaviour
{
    ClassType ClassSelection;
   // public GameData data;
    bool AppStarting = true;
    private static FMOD.Studio.EventInstance Ambience;
    private static FMOD.Studio.EventInstance Music;
    // Start is called before the first frame update
    void Awake()
    {       
        DontDestroyOnLoad(gameObject);
        Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Ambience_Outdoor");

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/DeepSpace");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);
            Music.start();

            Ambience.start();
            Ambience.release();
        }
        else if (SceneManager.GetActiveScene().name == "Hub")
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Odyssey");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);
            Music.start();
        }
        else if (SceneManager.GetActiveScene().name == "Arena")
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Digital Jungle");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);
            Music.start();

            Ambience.start();
            Ambience.release();
        }
        else if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TitleScreen");
            Music.setParameterByName("Intensity", 1.0f);
            Music.setVolume(0.2f);
            Music.start();
        }
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
