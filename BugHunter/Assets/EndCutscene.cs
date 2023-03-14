using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    public GameObject CutsceneCamera, Player;
    private void Awake()
    {
        Invoke(nameof(function),23.0f);
    }
    void function()
    {
        CutsceneCamera.SetActive(false);
        Player.SetActive(true);
    }
}
