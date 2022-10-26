using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas GameplayUI;
    public Canvas PauseMenuUI;
    public Canvas InventoryUI;
    public GameObject WeaponHolder;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.PausePlugin += PauseMenuEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void PauseMenuEnabled()
   {
        //disable gamepaly UI and enable Pause Menu UI when escape is pressed
        InventoryUI.enabled = false;
        GameplayUI.enabled = false;
        PauseMenuUI.enabled = true;
        WeaponHolder.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        //Time.timeScale = 0;
    }
    public void ResumeGame()
    {
      //  Debug.Log("ResumePressed");
        InventoryUI.enabled = true;
        GameplayUI.enabled = true;
        PauseMenuUI.enabled = false;
        WeaponHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
