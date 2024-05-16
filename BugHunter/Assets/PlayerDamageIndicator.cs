using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PlayerDamageIndicator : MonoBehaviour
{
    public GameObject HealthRegen;
    public Volume Volume;
    private HealthSystem healthSystem;
    [HideInInspector] public Vignette Vignette;
    [HideInInspector] public ChromaticAberration ChromaticAberration;
    private float VignetteIntensity = 0.0f,AbberationIntensity =0.1f;
    private void Awake()
    {
        Invoke(nameof(GetInfo), 0.5f);
    }
    void GetInfo()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnTakeDamage += TakeDamage;
    }
   
    void TakeDamage(int DMG)
    {
        if (DMG >= 0)
            return;

        if (Vignette == null)
            SetVignette();
        if (ChromaticAberration == null)
            SetCA();

        VignetteIntensity = 0.4f;
        AbberationIntensity = 0.3f;
        StartCoroutine(VignetteEffect());
        StartCoroutine(CrmAbbEffect());
    }
    public IEnumerator CrmAbbEffect()
    {
        while (AbberationIntensity > 0.0f)
        {
            yield return new WaitForSeconds(0.01f);
            AbberationIntensity -= 0.01f;
            ChromaticAberration.intensity.Override(AbberationIntensity);
        }
        ChromaticAberration.intensity.Override(0.0f);
    }
    public IEnumerator VignetteEffect()
    {
        while (VignetteIntensity > 0.0f)
        {
            yield return new WaitForSeconds(0.05f);
            VignetteIntensity -= 0.025f;
            Vignette.intensity.Override(VignetteIntensity);
        }
        Vignette.intensity.Override(0.0f);
    }
    public void SetEnvenomed(bool var)
    {
        if(var==false)
        Vignette.color.Override(Color.red);
       else if(var==true)
        Vignette.color.Override(Color.green);
    }
    void SetVignette()
    {
        Vignette tmp;
        if (Volume.profile.TryGet<Vignette>(out tmp))
            Vignette = tmp;
    }
    void SetCA()
    {
        ChromaticAberration tmp;
        if (Volume.profile.TryGet<ChromaticAberration>(out tmp))
            ChromaticAberration = tmp;
    }
    // Update is called once per frame

}
