using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas GameplayUI;
    public Canvas PauseMenuUI, OptionsUI;
    private bool toggle = true;
   // public Canvas InventoryUI;
    public GameObject WeaponHolder;
    public PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Awake()
    {
        GameplayUI = transform.parent.GetComponentInChildren<GUIHolder>().GUI.GetComponent<Canvas>();
        PauseMenuUI = transform.parent.GetComponentInChildren<GUIHolder>().PauseUI.GetComponent<Canvas>();
        PlayerInput.PausePlugin += PauseMenuEnabled;
        PlayerInput.PausePlugin += DisableOptionsMenu;
    }
    private void OnDestroy()
    {
        PlayerInput.PausePlugin -= PauseMenuEnabled;
        PlayerInput.PausePlugin -= DisableOptionsMenu;
    }
    // Update is called once per frame

    public void EnableOptionsMenu()
    {
        PauseMenuUI.enabled = false;
        OptionsUI.enabled = true;
    }
    public void DisableOptionsMenu()
    {

        if (OptionsUI.enabled == false)
            return;

        OptionsUI.enabled = false;
        PauseMenuUI.enabled = true;
    }
  
    void PauseMenuEnabled()
   {
        if (OptionsUI.enabled == true)
            return;
        //disable gamepaly UI and enable Pause Menu UI when escape is pressed
      //  OptionsUI.enabled = false;
        GameplayUI.enabled = !toggle;
        PauseMenuUI.enabled = toggle;
       // WeaponHolder.SetActive(false);
     

        transform.parent.GetComponentInChildren<WeaponInfo>().SetPaused(toggle);
        transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(toggle);
        transform.parent.GetComponentInChildren<Look>().SetIsPaused(toggle);

        toggle = !toggle;

        if (GameplayUI.enabled == true)
        {
          //ResumeGame();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (GameplayUI.enabled == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        
        //  Debug.Log("ResumePressed");
        // InventoryUI.enabled = true;
        GameplayUI.enabled = true;
        PauseMenuUI.enabled = false;
       // WeaponHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

        transform.parent.GetComponentInChildren<WeaponInfo>().SetPaused(false);
        transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(false);
        transform.parent.GetComponentInChildren<Look>().SetIsPaused(false);
        toggle = !toggle;
    }
    public void QuitGame()
    {
        Debug.LogWarning("tried to quit");
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
