using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMiniManager : MonoBehaviour
{
    FMOD.Studio.EventInstance HubTheme;
    // Start is called before the first frame update
    void Start()
    {
        HubTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Very Trashy");
        HubTheme.setVolume(0.1f);
        HubTheme.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying(HubTheme) == false)
        {
            HubTheme.start();
        }
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
