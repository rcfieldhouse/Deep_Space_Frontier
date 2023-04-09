using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
    
{
    public float cutsceneTime = 0.0f;
    Inputs PlayerInputController;
    public GameObject CutsceneCamera, Player;
    public GameObject cutscenecharacter1;
    public GameObject cutscenecharacter2;
    public GameObject cutscenecharacter3;
    public GameObject ship;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Invoke(nameof(function),cutsceneTime);
    }
    public void function()
    {
        CutsceneCamera.SetActive(false);
        Player.SetActive(true);
        Destroy(cutscenecharacter1);
        Destroy(cutscenecharacter2);
        Destroy(cutscenecharacter3);
        Destroy(ship);
        Destroy(transform.parent);

    }


}
