using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseCondition : MonoBehaviour
{
    private HealthSystem Health;
    private PlayerInput Player;
    public TextMeshProUGUI CountDownText;
    public bool isAlive = true; 

    private bool LoosePossible;

    private void Awake()
    {
        Player = GetComponent<PlayerInput>();
        Health = GetComponent<HealthSystem>();
        CountDownText = GameObject.Find("CountDownText").GetComponent<TextMeshProUGUI>();
        CountDownText.gameObject.SetActive(false);

        Player.Revive += Revive;
        Player.GiveUp += Loose;
        Health.OnObjectDeath += Die;
    }
    // Update is called once per frame
    void Die(GameObject obj)
    {
        GetComponent<PlayerAnimatorScript>().Die();
        GetComponent<CharacterController>().SwitchLadderCam(true);
        GetComponent<CharacterController>().WeaponCamera.SetActive(false);
        GetComponent<CharacterController>().PlayerCamera.SetActive(true);
        GetComponent<CharacterController>().SetSuspendMovement(true);

        isAlive = false;

        StartCoroutine(CountDown());
        
    }
    private IEnumerator CountDown()
    {
        CountDownText.gameObject.SetActive(true);

        for (int i =30; i>0; i--)
        {
            if (isAlive)
            {
                CountDownText.gameObject.SetActive(false);
                yield break;
            }
                

            CountDownText.text = i.ToString();

            yield return new WaitForSecondsRealtime(1);
        }
        

        GameManager.instance.SceneChange("Hub");
    }
    void Revive()
    {
        GetComponent<HealthSystem>().SetHealth(GetComponent<HealthSystem>().GetMaxHealth() / 2);
        GetComponent<PlayerAnimatorScript>().Revive();
        GetComponent<CharacterController>().SwitchLadderCam(false);
        GetComponent<CharacterController>().WeaponCamera.SetActive(true);
        GetComponent<CharacterController>().PlayerCamera.SetActive(false);
        GetComponent<CharacterController>().SetSuspendMovement(false);

        isAlive = true;
    }
    void Loose()
    {
        //Player.gameObject.GetComponent<LootHolder>().SaveData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
        GameManager.instance.SceneChange("Hub");
    }
}