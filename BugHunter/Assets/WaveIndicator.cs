using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WaveIndicator : MonoBehaviour
{
    public GameObject Background, Text;
    private void Awake()
    {
        ArenaManager.NewWave += DisplayNewWave;
        Text.AddComponent<TextMeshPro>();
    }
    void DisplayNewWave(int wave)
    {
        Background.SetActive(true);
        Text.SetActive(true);

        Text.GetComponent<TextMeshProUGUI>().alpha = 1.0f;
        var tempColor2 = Background.GetComponent<Image>().color;
        tempColor2.a = 1.0f;
        Background.GetComponent<Image>().color = tempColor2;
        Text.GetComponent<TextMeshProUGUI>().text = "Wave " + wave.ToString();

        StartCoroutine(FadeOut());
    }
   
    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);
      
        float num = 0;
        while (num < 1.0f)
        {
      
            Text.GetComponent<TextMeshProUGUI>().alpha = 1.0f - num;
            num += 0.01f;
         
            var tempColor = Background.GetComponent<Image>().color;
            tempColor.a = 1- num;
            Background.GetComponent<Image>().color = tempColor;
            Debug.Log(num);
            yield return new WaitForSeconds(0.01f);

        }
        Text.GetComponent<TextMeshProUGUI>().alpha = 0.0f;
        var tempColor2 = Background.GetComponent<Image>().color;
        tempColor2.a = 0.0f;
        Background.GetComponent<Image>().color = tempColor2;
    }
}
