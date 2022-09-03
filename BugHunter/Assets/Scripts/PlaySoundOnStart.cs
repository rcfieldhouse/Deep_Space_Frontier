using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        SoundManager.instance.PlaySound(_clip);
    }

}
