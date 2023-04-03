using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class TutorialController : MonoBehaviour
{
    // Main Canvas as well as each tutorial section UI Parent
    public GameObject TutorialCanvas;
    public GameObject ShootingTutorialUI;
    public GameObject UtilityTutorialUI;
    public GameObject MovementTutorialUI;

    // UI Parent for End Tutorial Popup
    public GameObject TutorialCompleteUI;

    // Reference to Player
    public GameObject Player;

    // Array of UI Checklist Elements for Shooting Section
    public GameObject[] ShootingUIArray;
    private int ShootingUIIndex;

    // Array of UI Checklist Elements for Utility Section
    public GameObject[] UtilityUIArray;
    private int UtilityUIIndex;

    // Array of UI Checklist Elements for Movement Section
    public GameObject[] MovementUIArray;
    private int MovementUIIndex;

    // Bools for when a section is complete
    private bool ShootingComplete = false;
    private bool UtilityComplete = false;
    private bool MovementComplete = false;

    public static TutorialController instance;

    FMOD.Studio.EventInstance IntroVoiceline;
    FMOD.Studio.EventInstance WeaponCheck;
    FMOD.Studio.EventInstance Reload;
    FMOD.Studio.EventInstance DodgeRoll;
    FMOD.Studio.EventInstance Explosives;
    FMOD.Studio.EventInstance SecondaryWeapon;
    FMOD.Studio.EventInstance SuitCali;
    FMOD.Studio.EventInstance NewHome;

    FMOD.Studio.PLAYBACK_STATE PlaybackState;

    private bool beginVoicelines = true;
    private bool beginSuitCalibration = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        instance = this;
        //Set the first section's Canvas to active
        ShootingTutorialUI.SetActive(true);
        //BeginTutorial();
        IntroVoiceline = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Good Morning Pioneer");
        IntroVoiceline.start();
        
    }

    public void BeginTutorial()
    {
        TutorialCanvas.SetActive(true);
    }
    public void TutorialComplete()
    {
        if(ShootingComplete == true && UtilityComplete == true && MovementComplete == true)
        {
            TutorialCompleteUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            //Tutorial UI is closed in Tutorial Complete CloseButton OnClick()
        }
    }

    //Checklist that tracks player input then updates UI for Shooting Section
    private void ShootingChecklist()
    {
        for (int i = 0; i < ShootingUIArray.Length; i++)
        {
            if (i == ShootingUIIndex)
            {
                ShootingUIArray[i].GetComponent<Image>().gameObject.SetActive(true);
            }
        }

        if (ShootingUIIndex == 0)
        {
            if (IsPlaying(IntroVoiceline) == false && beginVoicelines == true)
            {
                //IntroVoiceline.release();
               IntroVoiceline.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
               WeaponCheck = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Weapons Check");
               WeaponCheck.start();
               beginVoicelines = false;
            }
        }

            if (ShootingUIIndex == 4)
        {
            ShootingUIArray[4].GetComponent<Image>().gameObject.SetActive(true);
            ShootingComplete = true;
        }
    }
    //Checklist that tracks player input then updates UI for Utility Section
    private void UtilityCheckList()
    {
        if (ShootingComplete == true)
        {
            UtilityTutorialUI.SetActive(true);
            ShootingTutorialUI.SetActive(false);
            for (int i = 0; i < UtilityUIArray.Length; i++)
            {
                if (i == UtilityUIIndex)
                {
                    UtilityUIArray[i].GetComponent<Image>().gameObject.SetActive(true);
                }
            }

            if (UtilityUIIndex == 2)
            {
                UtilityUIArray[2].GetComponent<Image>().gameObject.SetActive(true);
                UtilityComplete = true;
            }
        }
    }
    //Checklist that tracks player input then updates UI for Movement Section
    private void MovementCheckList()
    {
        if (UtilityComplete == true)
        {
            MovementTutorialUI.SetActive(true);
            UtilityTutorialUI.SetActive(false);
            for (int i = 0; i < MovementUIArray.Length; i++)
            {
                if (i == MovementUIIndex)
                {
                    MovementUIArray[i].GetComponent<Image>().gameObject.SetActive(true);
                }
            }

           //if (MovementUIIndex == 0)
           //{
           //    if (IsPlaying(DodgeRoll) == false && beginSuitCalibration == true)
           //    {
           //        Debug.Log("DodgeRoll is done playing");
           //        FMODUnity.RuntimeManager.PlayOneShot("event:/VoiceLines/Suit Calibration");
           //        //SuitCali = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Suit Calibration");
           //        //SuitCali.start();
           //        beginSuitCalibration = false;
           //    }
           //}


            if (MovementUIIndex == 3)
            {
                MovementUIArray[3].GetComponent<Image>().gameObject.SetActive(true);
                MovementComplete = true;
                TutorialComplete();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the section hasn't been completed then call it (They are false by default)
        if(ShootingComplete == false)
        {
            ShootingChecklist();
        }
        if (UtilityComplete == false)
        {
            UtilityCheckList();
        }
        if(MovementComplete == false)
        {
            MovementCheckList();
        }
    }

    public void LeftMouseDown()
    {
        if(ShootingUIIndex == 0)
        {
            ShootingUIIndex++;
            ShootingTutorialUI.transform.GetChild(1).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
        }
    }
    public void RightMouseDown()
    {
        if (ShootingUIIndex == 1)
        {
             ShootingUIIndex++;
            ShootingTutorialUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);

            IntroVoiceline.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            IntroVoiceline.release();

            WeaponCheck.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            WeaponCheck.release();

            Reload = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Reload Weapons");
            Reload.start();
        }
    }
    public void RKeyDown()
    {
        if (ShootingUIIndex == 2)
        {
             ShootingUIIndex++;
            ShootingTutorialUI.transform.GetChild(3).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);

           Reload.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
           //Reload.release();
           SecondaryWeapon = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Secondary Gun");
           SecondaryWeapon.start();
        }
    }
    public void ScrollWheelDown()
    {
        if (ShootingUIIndex == 3)
        {
             ShootingUIIndex++;
            ShootingTutorialUI.transform.GetChild(4).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);

            SecondaryWeapon.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //Reload.release();
            Explosives = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Explosives");
            Explosives.start();
        }
    }
    public void QKeyDown()
    {
        if (UtilityUIIndex == 0 && ShootingComplete == true)
        {
             UtilityUIIndex++;
            UtilityTutorialUI.transform.GetChild(1).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);

            Explosives.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //Reload.release();
            DodgeRoll = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Dodge Roll");
            DodgeRoll.start();
        }
    }
    public void CKeyDown()
    {
        if (UtilityUIIndex == 1)
        {
            DodgeRoll.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //Reload.release();
            SuitCali = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/Suit Calibration");
            SuitCali.start();
            
            Reload.getPlaybackState(out PlaybackState);
                Debug.Log(PlaybackState);
    
            UtilityUIIndex++;
            UtilityTutorialUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
    
        }
    }

    public void LSheftKeyDown()
    {
        if (MovementUIIndex == 1)
        {
                MovementUIIndex++;
            MovementTutorialUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
        }
    }
    public void MoveKeyDown()
    {
        if (MovementUIIndex == 0 && UtilityComplete == true)
        {
                MovementUIIndex++;
            MovementTutorialUI.transform.GetChild(1).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
        }
    }
    public void SpacebarKeyDown()
    {
        if (MovementUIIndex == 2)
        {
                MovementUIIndex++;
            MovementTutorialUI.transform.GetChild(3).GetChild(0).GetComponent<Image>().gameObject.SetActive(false);

            SuitCali.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            SuitCali.release();
            NewHome = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/New Home");
            NewHome.start();
        }
    }
    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
