using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image HealthBar;

    [SerializeField]
    public GameObject HealthComponentOverride;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    [SerializeField]
    private FMOD.Studio.EventInstance HealthChange;

    private void Awake()
    {
        StartCoroutine(wait());
        HealthBar.color = Color.cyan;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPercent(pct));
    }

    private IEnumerator ChangeToPercent(float pct)
    {
        float preChangePercent = HealthBar.fillAmount;
        float elapsed = 0f;

        if(preChangePercent<=pct)
            PlayHealthSound(pct);

        while (elapsed<updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            HealthBar.fillAmount = Mathf.Lerp(preChangePercent, pct, elapsed / updateSpeedSeconds);
          
            yield return null;
        }
        HealthBar.color = Color.Lerp(Color.red, Color.cyan, pct);
        
        HealthBar.fillAmount = pct;
    }
    // Update is called once per frame
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        if (HealthComponentOverride != null)
            HealthComponentOverride.GetComponent<HealthSystem>().OnHealthPercentChanged += HandleHealthChanged;

        else
            GetComponentInParent<HealthSystem>().OnHealthPercentChanged += HandleHealthChanged;
    }

    void PlayHealthSound(float healthPercent)
    {
        HealthChange = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/HealthUp");
        HealthChange.start();
        HealthChange.release();
    }
}
