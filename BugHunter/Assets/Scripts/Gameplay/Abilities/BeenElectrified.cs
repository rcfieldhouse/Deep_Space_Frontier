using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeenElectrified : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Kill());
    }
    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this);
    }
}
