using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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



    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }

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


    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


}
