using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    private WaitForSeconds DecayTime = new WaitForSeconds(5.0f);
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(DecayThis());
    }
    public void SetDecayTime(float num)
    {
        DecayTime = new WaitForSeconds(num);
    }
    private IEnumerator DecayThis()
    {
        yield return DecayTime;
        gameObject.SetActive(false);
    }
}