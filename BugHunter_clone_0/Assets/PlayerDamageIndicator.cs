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
    private float VignetteIntensity = 0.0f;
    private void Awake()
    {
        Invoke(nameof(GetInfo), 0.5f);
    }
    void GetInfo()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnTakeDamage += TakeDamage;
    }
   
    void TakeDamage(int foo)
    {
        if (foo >= 0)
            return;

        if (Vignette == null)
            SetVignette();


        VignetteIntensity = 0.4f;
        StartCoroutine(VignetteEffect());
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
        Debug.Log("finished effect");
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
    // Update is called once per frame
  
}
