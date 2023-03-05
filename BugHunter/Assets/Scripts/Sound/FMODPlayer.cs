using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
    public static FMODPlayer _instance;
    private static FMOD.Studio.EventInstance Ambience;
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
    private FMODUnity.EventReference music;



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


    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }

        //Attach sound @ runtime                                
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerIntro, GetComponent<Transform>(), GetComponent<Rigidbody>());

        Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Ambience_Outdoor");
        //Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Level1");

        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        //SoundFXVolume = FMODUnity.RuntimeManager.GetBus("bus:/Sounds");
        //DialogueVolume = FMODUnity.RuntimeManager.GetBus("bus:/Dialogue");
        //MusicVolume = FMODUnity.RuntimeManager.GetBus("bus:/Music");

        Ambience.start();
        Ambience.release();

        Music.start();
        Music.release();
        Intensity(1);
    }

    private void Update()
    {
        volume = Mathf.Pow(10.0f, MasterVolume / 20f);
        MasterBus.setVolume(volume);
    }

    
    public void Intensity(float ProgressLevel)
    {
        Music.setParameterByName("Intensity", ProgressLevel);
    }

    public void EnemyProximity(float ProximityLevel)
    {

    }

    public void PlayerHealthPercent(float ProgressLevel)
    {

    }

    void StopAllPlayerEvents()
    {
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


}
