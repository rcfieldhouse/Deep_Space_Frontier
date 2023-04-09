using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            FMOD.Studio.EventInstance Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/DeepSpaceGuitar");
            Music.start();
            Music.release();
        }
    }

}
