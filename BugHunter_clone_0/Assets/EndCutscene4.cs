using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene4 : MonoBehaviour
{
    public GameObject Queen;
    private void Awake()
    {
        Invoke(nameof(EndScene),11.0f);
    }
    void EndScene()
    {
        Queen.SetActive(true);
        gameObject.SetActive(false);
    }
}
