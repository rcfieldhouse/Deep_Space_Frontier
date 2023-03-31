using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    // Start is called before the first frame update
    void Awake()
    {
        
        instance = this;
        //Set the first section's Canvas to active
        ShootingTutorialUI.SetActive(true);
        //BeginTutorial();
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

           //if (UtilityUIIndex == 0)
           //{
           //    if (Input.GetKeyDown(KeyCode.Q))
           //    {
           //        UtilityUIIndex++;
           //    }
           //}
           //if (UtilityUIIndex == 1)
           //{
           //    if (Input.GetKeyDown(KeyCode.C))
           //    {
           //        UtilityUIIndex++;
           //    }
           //}
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
           //    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
           //    {
           //        MovementUIIndex++;
           //    }
           //}
            //if (MovementUIIndex == 1)
            //{
            //    if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            //    {
            //            MovementUIIndex++;
            //    }
            //}
            //if (MovementUIIndex == 2)
            //{
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        MovementUIIndex++;
            //    }
            //}
            if(MovementUIIndex == 3)
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
        }
    }
    public void RightMouseDown()
    {
        if (ShootingUIIndex == 1)
        {
             ShootingUIIndex++;
        }
    }
    public void RKeyDown()
    {
        if (ShootingUIIndex == 2)
        {
             ShootingUIIndex++;
        }
    }
    public void ScrollWheelDown()
    {
        if (ShootingUIIndex == 3)
        {
             ShootingUIIndex++;
        }
    }
    public void QKeyDown()
    {
        if (UtilityUIIndex == 0 && ShootingComplete == true)
        {
             UtilityUIIndex++;
        }
    }
    public void CKeyDown()
    {
        if (UtilityUIIndex == 1)
        {
            UtilityUIIndex++;
        }
    }
    public void LSheftKeyDown()
    {
        if (MovementUIIndex == 1)
        {
                MovementUIIndex++;
        }
    }
    public void MoveKeyDown()
    {
        if (MovementUIIndex == 0 && UtilityComplete == true)
        {
                MovementUIIndex++;
        }
    }
    public void SpacebarKeyDown()
    {
        if (MovementUIIndex == 2)
        {
                MovementUIIndex++;
        }
    }
}
