using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WaveIndicator : MonoBehaviour
{
    public GameObject Background, Text;
    float num = 0;
    float TimeStopped = 0;
    private void Awake()
    {
        ArenaManager.NewWave += DisplayNewWave;
        Text.AddComponent<TextMeshProUGUI>();
    }
    void DisplayNewWave(int wave)
    {
        TimeStopped = Time.time;
        Background.SetActive(true);
        Text.SetActive(true);

        Text.GetComponent<TextMeshProUGUI>().alpha = 1.0f;
        var tempColor2 = Background.GetComponent<Image>().color;
        tempColor2.a = 1.0f;
        Background.GetComponent<Image>().color = tempColor2;
        Text.GetComponent<TextMeshProUGUI>().text = "Wave " + wave.ToString();
        num = 0;
        StartCoroutine(FadeOut());
       
    }
   
    public IEnumerator FadeOut()
    {
        if (num == 0.0f)
        {
            Debug.Log(("waiting"));
            yield return new WaitForSeconds(3.0f);
        }
       
      

        while (num < 1.0f)
        {
      
            Text.GetComponent<TextMeshProUGUI>().alpha = 1.0f - num;
            num += 0.01f;
         
            var tempColor = Background.GetComponent<Image>().color;
            tempColor.a = 1- num;
            Background.GetComponent<Image>().color = tempColor;
            yield return new WaitForSeconds(0.01f);

        }
        Text.GetComponent<TextMeshProUGUI>().alpha = 0.0f;
        var tempColor2 = Background.GetComponent<Image>().color;
        tempColor2.a = 0.0f;
        Background.GetComponent<Image>().color = tempColor2;
    }
    private void OnEnable()
    {
        num += Time.time - TimeStopped;
        StartCoroutine(FadeOut());
    }
    private void OnDisable()
    {
        
        TimeStopped = Time.time;
        StopAllCoroutines();
    }
    private void OnDestroy()
    {
        ArenaManager.NewWave -= DisplayNewWave;
    }
}
