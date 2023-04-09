using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
        Destroy(this);
    }
}
