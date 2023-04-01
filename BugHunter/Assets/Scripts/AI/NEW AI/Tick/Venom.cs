using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Venom : MonoBehaviour
{
    private float DamageTime = 5.0f, DamageInterval = 0.25f;
    public int Damage = -5;
    private float Tick = 0.0f;

    int TotalDamageDone = 0;
    // Start is called before the first frame update

 
    public void InitAttack(float Time, float Interval, int damage)
    {
        Damage = damage;
        DamageTime = Time;
        DamageInterval = Interval;
        gameObject.GetComponent<GUIHolder>().PoisonedSymbol.SetActive(true);

        TotalDamageDone =((int) (Time / Interval) * damage)+damage;
        Debug.Log(TotalDamageDone);
        Debug.Log((float)GetComponent<HealthSystem>().GetHealth());
        Debug.Log(((float)GetComponent<HealthSystem>().GetHealth()+TotalDamageDone)/(float)GetComponent<HealthSystem>().GetMaxHealth());
        GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).gameObject.SetActive(true);
        GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).GetComponent<Image>().color = Color.green;
        GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).GetComponent<Image>().fillAmount =
           ((float)GetComponent<HealthSystem>().GetHealth() + TotalDamageDone) / (float)GetComponent<HealthSystem>().GetMaxHealth();

        GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).GetComponent<PoisonHealthChange>().Health = GetComponent<HealthSystem>();
      GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).GetComponent<PoisonHealthChange>().DamageRemaining = TotalDamageDone;
       
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
                StartCoroutine(ChangeToPercent(1.0f -(Time/DamageTime)));
                // damage per burn tick
                GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).GetComponent<PoisonHealthChange>().DamageRemaining -= Damage;
                gameObject.GetComponent<HealthSystem>().ModifyHealth(gameObject, Damage);
                Tick += DamageInterval;
            }
          
            yield return null;
        }
        gameObject.GetComponent<GUIHolder>().PoisonedSymbol.SetActive(false);
        GetComponent<GUIHolder>().PoisonedSymbol.transform.parent.GetChild(1).gameObject.SetActive(false);
        //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        GetComponent<PlayerDamageIndicator>().SetEnvenomed(false);
        Destroy(this);
        yield return null;
    }
    private IEnumerator ChangeToPercent(float pct)
    {
        float preChangePercent = GetComponent<GUIHolder>().PoisonedSymbol.GetComponent<Image>().fillAmount;
        float elapsed = 0f;


        while (elapsed < DamageInterval/2.0f)
        {
            elapsed += Time.deltaTime;
           
            GetComponent<GUIHolder>().PoisonedSymbol.GetComponent<Image>().fillAmount = Mathf.Lerp(preChangePercent, pct, elapsed / (DamageInterval / 2.0f));

            yield return null;
        }

        GetComponent<GUIHolder>().PoisonedSymbol.GetComponent<Image>().fillAmount = pct;
    }
}
