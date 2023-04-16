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

    private static FMOD.Studio.VCA MasterVCA;
    private static FMOD.Studio.VCA AmbientVCA;
    private static FMOD.Studio.VCA SFXVCA;
    private static FMOD.Studio.VCA UIVCA;
    private static FMOD.Studio.VCA VoiceLineVCA;
    private static FMOD.Studio.VCA MusicVCA;


    [SerializeField]
    [Range(-80f, 10f)]
    private float MasterVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float SoundFXVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float AmbientVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float DialogueVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float MusicVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float UIVolume;

    private float volume;



    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }

        MasterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        VoiceLineVCA = FMODUnity.RuntimeManager.GetVCA("vca:/VoiceLines");
        SFXVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        MusicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        AmbientVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Ambience");
        UIVCA = FMODUnity.RuntimeManager.GetVCA("vca:/UI");

        //EventClass.OnSliderChanged += SetVolume;

    }

    private void Update()
    {
        volume = Mathf.Pow(10.0f, MasterVolume / 20f);
    }

    private void SetVolume()
    {
        MasterVCA.setVolume(MasterVolume);
        VoiceLineVCA.setVolume(DialogueVolume);
        SFXVCA.setVolume(SoundFXVolume);
        MusicVCA.setVolume(MusicVolume);
        AmbientVCA.setVolume(AmbientVolume);
        UIVCA.setVolume(UIVolume);
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
