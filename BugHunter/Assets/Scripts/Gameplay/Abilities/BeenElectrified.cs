using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeenElectrified : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Invoke(nameof(Kill), 2.0f);
    }
    private void Kill()
    {
        Destroy(this);
    }
}
