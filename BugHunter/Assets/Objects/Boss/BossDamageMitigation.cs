using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageMitigation : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(KillThis());
    }
    private IEnumerator KillThis()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
