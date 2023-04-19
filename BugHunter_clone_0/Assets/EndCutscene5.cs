using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene5 : MonoBehaviour
{
    public GameObject Cutscene5;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HealthSystem>().currentHealth > 0)
            return;

        Cutscene5.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
