using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHit : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(DestroyThis());
    }
    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(4.0f);
    }
}
