using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackFX : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> ScreenEffects;
    private float PercentHealth = 1.0f;
    private int _MaxHealth = 1;
    void Awake()
    {

        for (int i = 0; i <= 2; i++)
        {
            ScreenEffects.Add(gameObject.transform.parent.GetChild(1).GetChild(3).GetChild(i).gameObject);
        }
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
        ScreenEffects[0].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, PercentHealth);
        ScreenEffects[1].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, PercentHealth);
        ScreenEffects[2].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, PercentHealth);
    }

}
