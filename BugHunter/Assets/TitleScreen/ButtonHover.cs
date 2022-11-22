using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;



public class ButtonHover : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void start()
    {
        //text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeOnHover()
    {
        text.fontSharedMaterial.EnableKeyword("GLOW_ON");
        text.fontSharedMaterial.SetFloat("_GlowPower", 80f);
    }
    public void ChangeOnLeave()
    {
        text.material.DisableKeyword("GLOW_INNER");
        text.fontSharedMaterial.SetFloat("_GlowPower", 80f);
    }
}

