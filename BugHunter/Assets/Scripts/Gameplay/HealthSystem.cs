using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{


    [SerializeField] private int maxHealth = 500;
    private int currentHealth;

    public event Action<float> OnHealthPercentChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public float GetHealth()
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

    public void ModifyHealth(int amount)
    {

        currentHealth += amount;
        
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Current health is " + currentHealth);

        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        OnHealthPercentChanged(currentHealthPercent);

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, deactivate it 
            gameObject.SetActive(false);
        }
    }
}
