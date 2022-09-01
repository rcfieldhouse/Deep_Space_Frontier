using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float Health = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetHealth()
    {
        return Health;
    }
    public void SetHealth(float health)
    {
        Health = health;
    }
    public void takeDamage(float damage)
    {
        Health -= damage;
    }
    public void gainHealth(float health)
    {
        if (Health !< 500.0f)
        {
            Health += health;
        }     
    }
}
