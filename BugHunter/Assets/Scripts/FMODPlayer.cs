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
    private FMODUnity.EventReference uiDragEvent;

    [SerializeField]
    private FMODUnity.EventReference uiDropEvent;

    [SerializeField]
    private FMODUnity.EventReference uiHoverEvent;

    [SerializeField]

    private FMODUnity.EventReference uiPauseEvent;

    [SerializeField]
    private FMODUnity.EventReference uiUnpauseEvent;

    [SerializeField]
    private FMODUnity.EventReference uiUseEvent;

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


    void Start()
    {

        if (_instance == null)
        {
            _instance = new FMODPlayer();
        }

        //Attach sound @ runtime                                
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerIntro, GetComponent<Transform>(), GetComponent<Rigidbody>());

        Music = FMODUnity.RuntimeManager.CreateInstance("event:/TestEvent");
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


    public void PlayUIDragEvent()
    {
        if (uiDragEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiDragEvent);
        }
    }

    public void PlayUIDropEvent()
    {
        if (uiDropEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiDropEvent);
        }
    }

    public void PlayUIHoverEvent()
    {
        if (uiHoverEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiHoverEvent);
        }
    }

    public void PlayUIPauseEvent()
    {
        if (uiPauseEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiPauseEvent);
        }
    }

    public void PlayUIUnpauseEvent()
    {
        if (uiUnpauseEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiUnpauseEvent);
        }
    }

    public void PlayUIUseEvent()
    {
        if (uiUseEvent.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiUseEvent);
        }
    }
}
