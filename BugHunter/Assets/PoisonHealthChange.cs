using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonHealthChange : MonoBehaviour
{
    public int DamageRemaining = 0;
    [HideInInspector]  public HealthSystem Health;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = ((float)Health.GetHealth() + DamageRemaining) / Health.GetMaxHealth();
    }
}
