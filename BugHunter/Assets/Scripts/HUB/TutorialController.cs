using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public GameObject ShootingTutorialUI;
    public GameObject UtilityTutorialUI;
    public GameObject MovementTutorialUI;
    public GameObject Player;

    private bool ShootingComplete = false;
    private bool UtilityComplete = false;
    private bool MovementComplete = false;

    public static TutorialController instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        BeginTutorial();
    }

    public void BeginTutorial()
    {
        TutorialCanvas.SetActive(true);
        BeginShootingTutorial();
    }

    private void BeginShootingTutorial()
    {
        ShootingTutorialUI.SetActive(true);
        UtilityTutorialUI.SetActive(false);
        MovementTutorialUI.SetActive(false);


        //Track the player's Mouse 1 Button then mark it complete

        //Enable the Complete Icon on the first objective
        ShootingTutorialUI.transform.GetChild(1).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        ShootingTutorialUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        ShootingTutorialUI.transform.GetChild(3).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        ShootingTutorialUI.transform.GetChild(4).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
    }

    private void BeginUtilityTutorial()
    {
        ShootingTutorialUI.SetActive(false);
        UtilityTutorialUI.SetActive(true);
        MovementTutorialUI.SetActive(false);


        //Track the player's Mouse 1 Button then mark it complete

        //Enable the Complete Icon on the first objective
        UtilityTutorialUI.transform.GetChild(1).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        UtilityTutorialUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        UtilityTutorialUI.transform.GetChild(3).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        UtilityTutorialUI.transform.GetChild(4).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
