using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    private HealthSystem Health;
    private PlayerInput Player;
    private bool LoosePossible;
    private void Awake()
    {
        Player = GetComponent<PlayerInput>();
        Health = GetComponent<HealthSystem>();
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
    }
    void Revive()
    {
        GetComponent<HealthSystem>().SetHealth(GetComponent<HealthSystem>().GetMaxHealth() / 2);
        GetComponent<PlayerAnimatorScript>().Revive();
        GetComponent<CharacterController>().SwitchLadderCam(false);
        GetComponent<CharacterController>().WeaponCamera.SetActive(true);
        GetComponent<CharacterController>().PlayerCamera.SetActive(false);
        GetComponent<CharacterController>().SetSuspendMovement(false);
    }
    void Loose()
    {
        Player.gameObject.GetComponent<LootHolder>().SaveData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
        GameManager.instance.SceneChange("Hub");
    }
}