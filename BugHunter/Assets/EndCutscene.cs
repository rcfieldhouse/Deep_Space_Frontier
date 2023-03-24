using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour

    
{
    public float cutsceneTime = 0.0f;

    public GameObject CutsceneCamera, Player;
    private void Awake()
    {
        Invoke(nameof(function),cutsceneTime);
    }
    void function()
    {
        CutsceneCamera.SetActive(false);
        Player.SetActive(true);
    }
}
