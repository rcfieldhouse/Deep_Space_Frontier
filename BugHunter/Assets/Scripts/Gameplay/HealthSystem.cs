using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Netcode;

public class HealthSystem : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 500;
    public int currentHealth;
    public bool Invulnerable = false;
    public event Action<float> OnHealthPercentChanged = delegate { };
    public event Action<int> OnTakeDamage = delegate { };
    public event Action<GameObject> OnObjectDeath = delegate { };
    public event Action<Transform> OnObjectDeathT = delegate { };
    [Header("Low Health Vignette")]
    static public VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.Vignette vignette;
    public float fadeInTime = 0.5f;

    NetworkVariable<int> networkHealth = new NetworkVariable<int>(200);

    private void OnEnable()
    {  
        if (gameObject.tag == "Player")
            gameObject.AddComponent<LoseCondition>();


        networkHealth.OnValueChanged +=  OnNetworkHealthChanged;
        currentHealth = maxHealth;
        
    }
    override public void OnNetworkSpawn()
    {
        networkHealth.Value = maxHealth;
    }
    private void OnDisable()
    {
        networkHealth.OnValueChanged -= OnNetworkHealthChanged;
    }
    public void SetInvulnerable(bool inv)
    {
        Invulnerable = inv;
    }
    public int GetHealth()
    {
        return networkHealth.Value;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        networkHealth.Value = currentHealth;
        float currentHealthPercent = (float)networkHealth.Value / (float)maxHealth;
        OnHealthPercentChanged(currentHealthPercent);
    }
    private void Update()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        networkHealth.Value = currentHealth;
    }
    public void SetMaxHealth(int Health)
    {
        maxHealth = Health;
        currentHealth = Health;
    }

    [ServerRpc(RequireOwnership = false)]
    void NetworkHealthServerRpc(int amount)
    {
        Debug.Log("NetworkHealthServerRpc Called");

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        networkHealth.Value = currentHealth;

        float currentHealthPercent = (float)networkHealth.Value / (float)maxHealth;

        OnHealthPercentChanged(currentHealthPercent);
        Debug.Log("[server] currentHealthPercent: " + currentHealthPercent);

        //Check if health has fallen below zero
        if (networkHealth.Value <= 0.0f)
        {

            Debug.Log(transform.name+" Successfully Despawned");

            OnObjectDeath?.Invoke(transform.gameObject);
            OnObjectDeathT?.Invoke(transform);
        }
    }

    public void OnNetworkHealthChanged(int previous, int current)
    {
        //Debug.Log("[Client] OnNetworkHealthChanged Called");
        currentHealth = networkHealth.Value;
        float currentHealthPercent = (float)networkHealth.Value / (float)maxHealth;
       
        OnHealthPercentChanged(currentHealthPercent);

        //Check if health has fallen below zero
        if (networkHealth.Value <= 0.0f)
        {
            OnObjectDeath?.Invoke(transform.gameObject);
            OnObjectDeathT?.Invoke(transform);
        }

    }

    private int HandleDamageModifiers(GameObject requester, int amount)
    {
        EquipmentManager equipment = transform.GetComponent<EquipmentManager>();

        //Debug.Log("Damage is: " + amount + " PRE-mitigation, from " + requester.name);

        return equipment.ExecuteEquip(requester, amount);
    }

    public void ModifyHealth(GameObject requester, int amount)
    {
        if (Invulnerable == false && currentHealth >= 0)
        {
            if (requester != null)
                amount = HandleDamageModifiers(requester, amount);

            NetworkHealthServerRpc(amount);

            if(transform.tag == "Player")
            {
                currentHealth += amount;               
                networkHealth.Value = currentHealth;
            
                float currentHealthPercent = (float)networkHealth.Value / (float)maxHealth;
                Debug.Log("Current Health Percent: " + currentHealthPercent);
                OnHealthPercentChanged(currentHealthPercent);
            
                //Check if health has fallen below zero
                if (networkHealth.Value <= 0.0f)
                {
                    OnObjectDeath?.Invoke(transform.gameObject);
                    OnObjectDeathT?.Invoke(transform);
                }
            }
            OnTakeDamage(amount);

        }
    }

    public void ModifyHealth(Transform requester, int amount)
    {
        if (Invulnerable == false && currentHealth >= 0)
        {
            NetworkHealthServerRpc(amount);
            if (transform.tag == "Player")
            {
                currentHealth += amount;

                networkHealth.Value = currentHealth;

                float currentHealthPercent = (float)networkHealth.Value / (float)maxHealth;
                Debug.Log("Current Health Percent: " + currentHealthPercent);
                OnHealthPercentChanged(currentHealthPercent);

                //Check if health has fallen below zero
                if (networkHealth.Value <= 0.0f)
                {
                    OnObjectDeath?.Invoke(transform.gameObject);
                    OnObjectDeathT?.Invoke(transform);
                }
            }
            OnTakeDamage(amount);
        }
    }
    public void ModifyHealth(int amount)
    {
        if (Invulnerable == false && currentHealth >= 0)
        {           
            NetworkHealthServerRpc(amount);
            if (transform.tag == "Player")
            {
                currentHealth += amount;
                Debug.Log("Current Health: " + currentHealth);
                Debug.Log("networkHealth: " + networkHealth.Value);

                networkHealth.Value = currentHealth;

                float currentHealthPercent = (float)networkHealth.Value / (float)maxHealth;
                Debug.Log("Current Health Percent: " + currentHealthPercent);
                OnHealthPercentChanged(currentHealthPercent);

                //Check if health has fallen below zero
                if (networkHealth.Value <= 0.0f)
                {
                    OnObjectDeath?.Invoke(transform.gameObject);
                    OnObjectDeathT?.Invoke(transform);
                }
            }
            OnTakeDamage(amount);

        }
    }
}