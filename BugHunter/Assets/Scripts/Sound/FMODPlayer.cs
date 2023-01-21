using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
    public static FMODPlayer _instance;
    private static FMOD.Studio.EventInstance Music;
    private static FMOD.Studio.Bus MasterBus;
    private static FMOD.Studio.Bus SoundFXBus;
    private static FMOD.Studio.Bus DialogueBus;
    private static FMOD.Studio.Bus MusicBus;

    [SerializeField]
    private FMODUnity.EventReference uiUpEvent;

    [SerializeField]
    private FMODUnity.EventReference uiDownEvent;

    [SerializeField]
    private FMODUnity.EventReference uiSelectEvent;

    [SerializeField]

    private FMODUnity.EventReference uiDeselectEvent;



    [SerializeField]
    [Range(-80f, 10f)]
    private float MasterVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float SoundFXVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float DialogueVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float MusicVolume;

    private float volume;

    public FMODUnity.EventReference PlayerStateEvent;

    public void PlaySound(string path)
    {
        FMODUnity.EventReference a;
        a.Path = path;
    }
    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }

        //Attach sound @ runtime                                
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerIntro, GetComponent<Transform>(), GetComponent<Rigidbody>());

        //Music = FMODUnity.RuntimeManager.CreateInstance("event:/TestEvent");
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        //SoundFXVolume = FMODUnity.RuntimeManager.GetBus("bus:/Sounds");
        //DialogueVolume = FMODUnity.RuntimeManager.GetBus("bus:/Dialogue");
        //MusicVolume = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        
        Music.start();
        Music.release();
    }

    private void Update()
    {
        volume = Mathf.Pow(10.0f, MasterVolume / 20f);
        MasterBus.setVolume(volume);
    }

    //this will be for intensity levels
    //public void Progress(float ProgressLevel)
    //{
    //    Music.setParameterByName("Progress", ProgressLevel);
    //}

    void StopAllPlayerEvents()
    {
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    public void PlayUIUpEvent()
    {
        if (uiUpEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiUpEvent);
        }
    }

    public void PlayUIDownEvent()
    {
        if (uiDownEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiDownEvent);
        }
    }

    public void PlayUISelectEvent()
    {
        Debug.Log("Doot");
        if (!uiSelectEvent.IsNull)
        {
            Debug.Log("Doot");
            FMODUnity.RuntimeManager.PlayOneShot(uiSelectEvent);
        }
    }

    public void PlayUIDeselectEvent()
    {
        Debug.Log("Doot");
        if (!uiDeselectEvent.IsNull)
        {
            Debug.Log("Doot");
            FMODUnity.RuntimeManager.PlayOneShot(uiDeselectEvent);
        }
    }

}
