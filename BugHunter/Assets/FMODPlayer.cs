using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
    private static FMOD.Studio.EventInstance Music;
    private static FMOD.Studio.Bus bus;

    [SerializeField]
    [Range(-80f, 10f)]
    private float busVolume;
    private float volume;

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/TestEvent");
        bus = FMODUnity.RuntimeManager.GetBus("bus:/");
        Music.start();
        Music.release();
    }

    private void Update()
    {
        volume = Mathf.Pow(10.0f, busVolume / 20f);
        bus.setVolume(volume);
    }
    //this will be for intensity levels
    //public void Progress(float ProgressLevel)
    //{
    //    Music.setParameterByName("Progress", ProgressLevel);
    //}

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
