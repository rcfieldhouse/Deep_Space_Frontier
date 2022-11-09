using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private GameObject HealthComponentOverride;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    private void Awake()
    {
        if(HealthComponentOverride!=null)
            HealthComponentOverride.GetComponent<HealthSystem>().OnHealthPercentChanged += HandleHealthChanged;
        
        else
            GetComponentInParent<HealthSystem>().OnHealthPercentChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPercent(pct));
    }

    private IEnumerator ChangeToPercent(float pct)
    {
        float preChangePercent = HealthBar.fillAmount;
        float elapsed = 0f;

        while (elapsed<updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            HealthBar.fillAmount = Mathf.Lerp(preChangePercent, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        HealthBar.fillAmount = pct;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //this is for in-world UI rotation

        //transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }
}
