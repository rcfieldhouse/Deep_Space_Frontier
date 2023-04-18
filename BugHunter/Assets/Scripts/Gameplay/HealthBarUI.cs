using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Unity.Netcode;

public class HealthBarUI : NetworkBehaviour
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

        //StartCoroutine(wait());
        if (IsBarrierHPBar == false)
        {
            HealthBar.color = Color.cyan;
        }
    }

    override public void OnNetworkSpawn()
    {
        StartCoroutine(wait()); 

    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);

        if (HealthComponentOverride != null)
            HealthComponentOverride.GetComponent<HealthSystem>().OnHealthPercentChanged += HandleHealthChanged;
        else
            GetComponentInParent<HealthSystem>().OnHealthPercentChanged += HandleHealthChanged;
        Debug.Log(gameObject.transform.parent.name + " HealthBar Initialized");

    }

    private void HandleHealthChanged(float pct)
    {

        Debug.Log(gameObject.transform.parent.name + "HandleHealthChanged UI Called");

        StartCoroutine(ChangeToPercent(pct));


            //HealthBar.fillAmount = pct;
    }

    private void OnDisable()
    {

        if (HealthComponentOverride != null)
            HealthComponentOverride.GetComponent<HealthSystem>().OnHealthPercentChanged -= HandleHealthChanged;
        else
            GetComponentInParent<HealthSystem>().OnHealthPercentChanged -= HandleHealthChanged;

    }
    private IEnumerator ChangeToPercent(float pct)
    {
        Debug.Log(gameObject.transform.parent.name + " ChangeToPercent");
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

    void PlayHealthSound(float healthPercent)
    {
        HealthChange = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/HealthUp");
        HealthChange.start();
        HealthChange.release();
    }
}
