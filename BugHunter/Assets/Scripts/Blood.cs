using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> BloodEffects;
    private float PercentHealth=1.0f;
    private int _MaxHealth=1;
    void Start()
    {
     
        for (int i = 0; i <= 2; i++)
        {
           BloodEffects.Add(gameObject.transform.parent.GetChild(1).GetChild(3).GetChild(i).gameObject);
        }       
    }
    private void Awake()
    {
        StartCoroutine(wait());
    }
    private IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
        _MaxHealth = GetComponent<HealthSystem>().GetHealth();
    }
    // Update is called once per frame
    void Update()
    {
      
        PercentHealth = (float)GetComponent<HealthSystem>().GetHealth() / (float)_MaxHealth;
        BloodEffects[0].GetComponent<SpriteRenderer>().color=Color.Lerp(Color.white,Color.clear,PercentHealth);
        BloodEffects[1].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, PercentHealth);
        BloodEffects[2].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, PercentHealth);
    }
}
