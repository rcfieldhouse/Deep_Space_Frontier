using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas GameplayUI;
    public Canvas PauseMenuUI;
    private bool toggle = true;
   // public Canvas InventoryUI;
    public GameObject WeaponHolder;
    // Start is called before the first frame update
    void Awake()
    {
        GameplayUI = transform.parent.GetComponentInChildren<GUIHolder>().GUI.GetComponent<Canvas>();
        PauseMenuUI = transform.parent.GetComponentInChildren<GUIHolder>().PauseUI.GetComponent<Canvas>();
        PlayerInput.PausePlugin += PauseMenuEnabled;
    }
    private void OnDestroy()
    {
        PlayerInput.PausePlugin -= PauseMenuEnabled;
    }
    // Update is called once per frame


    void PauseMenuEnabled()
   {
        //disable gamepaly UI and enable Pause Menu UI when escape is pressed
 
        GameplayUI.enabled = !toggle;
        PauseMenuUI.enabled = toggle;
       // WeaponHolder.SetActive(false);
     

        transform.parent.GetComponentInChildren<WeaponInfo>().SetPaused(toggle);
        transform.parent.GetComponentInChildren<WeaponInfo>().SetIsReloading(toggle);
        transform.parent.GetComponentInChildren<Look>().SetIsPaused(toggle);

        toggle = !toggle;

        if (GameplayUI.enabled == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log(" locked");
        }
        else if (GameplayUI.enabled == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log(" not");
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
