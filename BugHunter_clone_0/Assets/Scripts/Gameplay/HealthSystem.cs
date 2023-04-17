using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Netcode;

public class HealthSystem : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 500;
    public int currentHealth;
    private bool Invulnerable = false;
    public event Action<float> OnHealthPercentChanged = delegate { };
    public event Action<int> OnTakeDamage = delegate { };
    public event Action<GameObject> OnObjectDeath = delegate { };
    public event Action<Transform> OnObjectDeathT = delegate { };
    [Header("Low Health Vignette")]
    static public VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.Vignette vignette;
    public float fadeInTime = 0.5f;

    private void OnEnable()
    {
   
        if (gameObject.tag == "Player")
            gameObject.AddComponent<LoseCondition>();

        currentHealth = maxHealth;
    }
    public void SetInvulnerable(bool foo)
    {
        Invulnerable = foo;
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        OnHealthPercentChanged(currentHealthPercent);
    }
    public void SetMaxHealth(int foo)
    {
        maxHealth = foo;
        currentHealth = foo;
    }

    private int HandleDamageModifiers(GameObject requester, int amount)
    {
        EquipmentManager equipment = transform.GetComponent<EquipmentManager>();

        Debug.Log("Damage is: " + amount + " PRE-mitigation, from " + requester.name);

        return equipment.ExecuteEquip(requester, amount);
    }

    public void ModifyHealth(GameObject requester, int amount)
    {
        if (Invulnerable == false && currentHealth >= 0)
        {
    
            if (requester != null)
                amount = HandleDamageModifiers(requester, amount);
    
            //play Dante.sound.ogg all things to do with health 
    
            //could in theory just use a statement if being damaged or healed 
            currentHealth = currentHealth + amount;
    
                
            if (currentHealth > maxHealth) currentHealth = maxHealth;
    
            float currentHealthPercent = (float)currentHealth / (float)maxHealth;
            OnHealthPercentChanged(currentHealthPercent);
            OnTakeDamage(amount);
            //Check if health has fallen below zero
            if (currentHealth <= 0.0f)
                OnObjectDeath?.Invoke(requester);
            
        }
    }
    public void ModifyHealth(Transform requester, int amount)
    {
        if (Invulnerable == false && currentHealth >= 0)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            float currentHealthPercent = (float)currentHealth / (float)maxHealth;

            OnHealthPercentChanged(currentHealthPercent);
            OnTakeDamage(amount);
            //Check if health has fallen below zero
            if (currentHealth <= 0.0f)
            {
                //Broadcast that the object has died
                //   Destroy(gameObject);
                OnObjectDeathT?.Invoke(requester);


                //gameObject.SetActive(false);
            }
        }
    }
    public void ModifyHealth(int amount)
    {

        if (Invulnerable == false && currentHealth >= 0)
        {
            //play Dante.sound.ogg all things to do with health 
            //could in theory just use a statement if being damaged or healed 
            Debug.Log("Damage is: " + amount + " PRE-mitigation");
            currentHealth += amount;

            if (currentHealth > maxHealth) currentHealth = maxHealth;

            float currentHealthPercent = (float)currentHealth / (float)maxHealth;
            OnHealthPercentChanged(currentHealthPercent);
            OnTakeDamage(amount);
            //Check if health has fallen below zero
            if (currentHealth <= 0.0f)
            {
                //Broadcast that the object has died
                //   Destroy(gameObject);
                OnObjectDeath?.Invoke(transform.gameObject);

                //gameObject.SetActive(false);
            }
        }
    }
}