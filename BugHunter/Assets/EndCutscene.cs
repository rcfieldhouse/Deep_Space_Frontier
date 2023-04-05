using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
    
{
    public float cutsceneTime = 0.0f;
    Inputs PlayerInputController;
    public GameObject CutsceneCamera, Player;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Invoke(nameof(function),cutsceneTime);
    }
    public void function()
    {
        CutsceneCamera.SetActive(false);
        Player.SetActive(true);
        Destroy(transform.parent);
    }


}
