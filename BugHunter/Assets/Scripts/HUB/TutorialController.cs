using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    private PlayerInput PlayerInput;

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
        PlayerInput = Player.GetComponent<PlayerInput>();
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
                //ShootingUIArray[i].SetActive(true);
                ShootingUIArray[i].GetComponent<Image>().gameObject.SetActive(true);
            }
            //else
            //{
            //    //ShootingUIArray[i].SetActive(false);
            //    ShootingUIArray[i].GetComponent<Image>().gameObject.SetActive(false);
            //}
        }

        if (ShootingUIIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ShootingUIIndex++;
            }
        }
        if (ShootingUIIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ShootingUIIndex++;
            }
        }
        if (ShootingUIIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ShootingUIIndex++;
            }
        }
        if (ShootingUIIndex == 3)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                ShootingUIIndex++;
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
                    //ShootingUIArray[i].SetActive(true);
                    UtilityUIArray[i].GetComponent<Image>().gameObject.SetActive(true);
                }
                //else
                //{
                //    //ShootingUIArray[i].SetActive(false);
                //    ShootingUIArray[i].GetComponent<Image>().gameObject.SetActive(false);
                //}
            }

            if (UtilityUIIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    UtilityUIIndex++;
                }
            }
            if (UtilityUIIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    UtilityUIIndex++;
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
                    //ShootingUIArray[i].SetActive(true);
                    MovementUIArray[i].GetComponent<Image>().gameObject.SetActive(true);
                }
                //else
                //{
                //    //ShootingUIArray[i].SetActive(false);
                //    ShootingUIArray[i].GetComponent<Image>().gameObject.SetActive(false);
                //}
            }

            if (MovementUIIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    MovementUIIndex++;
                }
            }
            if (MovementUIIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                        MovementUIIndex++;
                }
            }
            if (MovementUIIndex == 2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    MovementUIIndex++;
                }
            }
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

    private void LeftMouseDown()
    {

    }
    private void RightMouseDown()
    {

    }
    private void RKeyDown()
    {

    }
    private void ScrollWheelDown()
    {

    }
    private void QKeyDown()
    {

    }
    private void CKeyDown()
    {

    }
    private void LSheftKeyDown()
    {

    }
    private void WKeyDown()
    {

    }
    private void AKeyDown()
    {

    }
    private void SKeyDown()
    {

    }
    private void DKeyDown()
    {

    }
    private void SPacebarKeyDown()
    {  

    }
}
