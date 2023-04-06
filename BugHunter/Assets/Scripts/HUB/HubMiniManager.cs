using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMiniManager : MonoBehaviour
{
    FMOD.Studio.EventInstance Music;
    // Start is called before the first frame update
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Odyssey");
        Music.setParameterByName("Intensity", 1.0f);
        Music.setVolume(0.2f);
        Music.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying(Music) == false)
        {
            Music.start();
        }
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
