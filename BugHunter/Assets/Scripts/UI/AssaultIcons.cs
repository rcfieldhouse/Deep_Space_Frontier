using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssaultIcons : MonoBehaviour
{
    [SerializeField]
    private Image ClassAbility;

    // Start is called before the first frame update
    void Awake()
    {
        ClassAbility.fillAmount = 1.0f;
        transform.GetChild(0).gameObject.SetActive(false);
        Dodge.Dodged+=UseDodge;
    }
    private void OnDestroy()
    {
        Dodge.Dodged -= UseDodge;
    }
    void UseDodge(float foo)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        ClassAbility.fillAmount = 0.0f;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Dodge_Roll");
        StartCoroutine(RechargeAbility(foo)); 
    }
    private IEnumerator RechargeAbility(float RechargeTime)
    {
        float fillTime = 0;

        while (fillTime < RechargeTime)
        {
            fillTime+=Time.deltaTime;
            ClassAbility.fillAmount = Mathf.Lerp(0.0f,1.0f,fillTime/RechargeTime);
            yield return null;
        }
       
    }
    // Update is called once per frame
 
}
