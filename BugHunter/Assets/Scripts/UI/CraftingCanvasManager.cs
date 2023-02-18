using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCanvasManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject DefaultTab, DefaultLeftTab;

    public GameObject AssaultWeaponUI, EngineerWeaponUI, SniperWeaponUI;
    public GameObject AssaultUpgradeUI, EngineerUpgradeUI, SniperUpgradeUI;


    // Start is called before the first frame update
    void Start()
    {
        //set the canvas to inactive once the game starts
        gameObject.SetActive(false);

        // set the scroll view tab to default active
        DefaultTab.SetActive(true);
        DefaultLeftTab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPlayerClass()
    {
        if (Player.GetComponent<ClassCreator>().GetClass() == ClassType.Assault)
        {
            enableAssaultUI();
        }
        else if (Player.GetComponent<ClassCreator>().GetClass() == ClassType.Engineer)
        {
            enableEngineerUI();
        }
        else if (Player.GetComponent<ClassCreator>().GetClass() == ClassType.Sniper)
        {
            enableSniperUI();
        }
        //Debug.Log("Class Type is " + Player.GetComponent<ClassCreator>().GetClass());
    }

    public void enableAssaultUI()
    {
        // enable Assault UI
        AssaultWeaponUI.SetActive(true);
        AssaultUpgradeUI.SetActive(true);

        // Disable Engineer & Sniper UI
        EngineerWeaponUI.SetActive(false);
        EngineerUpgradeUI.SetActive(false);

        SniperWeaponUI.SetActive(false);
        SniperUpgradeUI.SetActive(false);
    }

    public void enableEngineerUI()
    {
        // enable Engineer UI
        EngineerWeaponUI.SetActive(false);
        EngineerUpgradeUI.SetActive(false);

        // Disable Assault & Sniper UI
        AssaultWeaponUI.SetActive(true);
        AssaultUpgradeUI.SetActive(true);

        SniperWeaponUI.SetActive(false);
        SniperUpgradeUI.SetActive(false);
    }

    public void enableSniperUI()
    {
        // enable Sniper UI
        SniperWeaponUI.SetActive(false);
        SniperUpgradeUI.SetActive(false);
        
        // Disable Assault & Engineer UI
        AssaultWeaponUI.SetActive(true);
        AssaultUpgradeUI.SetActive(true);

        EngineerWeaponUI.SetActive(false);
        EngineerUpgradeUI.SetActive(false);

    }
}
