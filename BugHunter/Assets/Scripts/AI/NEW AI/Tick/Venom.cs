using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{
    private float DamageTime = 5.0f, DamageInterval = 0.25f;
    public int Damage = -5;
    private float Tick = 0.0f;
    // Start is called before the first frame update

 
    public void InitAttack(float Time, float Interval, int damage)
    {
        Damage = damage;
        DamageTime = Time;
        DamageInterval = Interval;
        gameObject.GetComponent<GUIHolder>().PoisonedSymbol.SetActive(true);
        StartCoroutine(BurnDeBoi());
    }
    // Update is called once per frame

    private IEnumerator BurnDeBoi()
    {
      
        GetComponent<PlayerDamageIndicator>().SetEnvenomed(true);
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
         float Time=0; 
        while (Time < DamageTime)
        {
            Time += UnityEngine.Time.deltaTime;
            if (Time > Tick)
            {
                // damage per burn tick
                gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
                Tick += DamageInterval;
            }
            yield return null;
        }
        gameObject.GetComponent<GUIHolder>().PoisonedSymbol.SetActive(false);
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        GetComponent<PlayerDamageIndicator>().SetEnvenomed(false);
        Destroy(this);
        yield return null;
    }
}
