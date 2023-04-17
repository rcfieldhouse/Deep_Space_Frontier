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

    public bool IsBarrierHPBar = false;

    private void Awake()
    {
        StartCoroutine(wait());
        if (IsBarrierHPBar == false)
        {
            HealthBar.color = Color.cyan;
        }
    }

    private void HandleHealthChanged(float pct)
    {
        if (gameObject.activeInHierarchy == true)
            StartCoroutine(ChangeToPercent(pct));
        else
            HealthBar.fillAmount = pct;
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

            if (IsBarrierHPBar == false)
            {
                HealthBar.color = Color.Lerp(Color.red, Color.cyan, elapsed / updateSpeedSeconds);
            }

            yield return null;
        }

        
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
