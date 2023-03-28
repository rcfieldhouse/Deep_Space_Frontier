using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLockerCanvasManager : MonoBehaviour
{
    public GameObject Player;
    private int ChoiceArmour=0;
    public GameObject EquipPopup;
    public GameObject EquipButton;
    // UI Parents for each class's primary and secondary weapons
    public GameObject AssaultWeaponUI, EngineerWeaponUI, SniperWeaponUI, ArmorDisplay, WeaponDisplay;
    // UI Parents for each class Upgrades
    public GameObject AssaultUpgrades, EngineerUpgrades, SniperUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        CheckPlayerClass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPlayerClass()
    {
        Debug.Log("Class Type is " + Player.GetComponent<ClassCreator>().GetClass());
        if (Player.GetComponent<ClassCreator>().GetClass() == ClassType.Assault)
        {
            //Enable Assault UI and Assault Upgrades UI
            enableUI(AssaultWeaponUI, EngineerWeaponUI, SniperWeaponUI);
            enableUI(AssaultUpgrades, EngineerUpgrades, SniperUpgrades);
        }
        else if (Player.GetComponent<ClassCreator>().GetClass() == ClassType.Engineer)
        {
            //Enable Engineer UI and Engineer Upgrades UI
            enableUI(EngineerWeaponUI, AssaultWeaponUI, SniperWeaponUI);
            enableUI(EngineerUpgrades, AssaultUpgrades, SniperUpgrades);
        }
        else if (Player.GetComponent<ClassCreator>().GetClass() == ClassType.Sniper)
        {
            //Enable Sniper UI and Sniper Upgrades UI
            enableUI(SniperWeaponUI, AssaultWeaponUI, EngineerWeaponUI);
            enableUI(SniperUpgrades, AssaultUpgrades, EngineerUpgrades);
        }
    }
    public void EquipArmour()
    {
        switch (ChoiceArmour)
        {
            case 0:
                if (Player.GetComponent<EquipmentManager>().slime_armor.isEquippable == true)
                {
                    Player.GetComponent<EquipmentManager>().ChangeEquip(Player.GetComponent<EquipmentManager>().slime_armor);
                    //Enable Popup Message
                    EquipPopup.SetActive(true);
                }
                break;
            case 1:
                if (Player.GetComponent<EquipmentManager>().TickArmor.isEquippable == true)
                {
                    Player.GetComponent<EquipmentManager>().ChangeEquip(Player.GetComponent<EquipmentManager>().TickArmor);
                    //Enable Popup Message
                    EquipPopup.SetActive(true);
                }
                break;
            case 2:
                if (Player.GetComponent<EquipmentManager>().ZephyrArmor.isEquippable == true)
                {
                    Player.GetComponent<EquipmentManager>().ChangeEquip(Player.GetComponent<EquipmentManager>().ZephyrArmor);
                    //Enable Popup Message
                    EquipPopup.SetActive(true);
                }
                break;
            case 3:
                if (Player.GetComponent<EquipmentManager>().BomberArmor.isEquippable == true)
                {
                    Player.GetComponent<EquipmentManager>().ChangeEquip(Player.GetComponent<EquipmentManager>().BomberArmor);
                    //Enable Popup Message
                    EquipPopup.SetActive(true);
                }
                break;
        }
    }
    public void SetArmourChoice(int num)
    {
        ChoiceArmour = num;
    }
    public void enableUI(GameObject enableUI, GameObject disableUI1, GameObject disableUI2)
    {
        // enable the target UI
        enableUI.SetActive(true);
        // disable the other UIs
        disableUI1.SetActive(false);
        disableUI2.SetActive(false);
    }

    public void SwitchToArmorTab()
    {
        ArmorDisplay.SetActive(true);
        WeaponDisplay.SetActive(false);
    }
    public void SwitchToWeaponTab()
    {
        ArmorDisplay.SetActive(false);
        WeaponDisplay.SetActive(true);
    }

    public void ButtonOnMouse()
    {
        Vector3 MousePos = Input.mousePosition;

        EquipButton.SetActive(true);
        EquipButton.transform.position = MousePos;
    }
}
