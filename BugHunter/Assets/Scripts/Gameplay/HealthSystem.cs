using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{


    [SerializeField] private int maxHealth = 500;
    private int currentHealth;
    private bool Invulnerable = false;
    public event Action<float> OnHealthPercentChanged = delegate { };
    public event Action<GameObject> OnObjectDeath = delegate { };

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
    public void ModifyHealth(int amount)
    {
        if (Invulnerable == false) { 
               currentHealth += amount;
        
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            Debug.Log("Current health is " + currentHealth);

            float currentHealthPercent = (float)currentHealth / (float)maxHealth;
            OnHealthPercentChanged(currentHealthPercent);

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
