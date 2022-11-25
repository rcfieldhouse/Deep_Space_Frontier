using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.GetComponent<HealthSystem>().ModifyHealth(-125);
        Destroy(this);
        //prolly just do damage 
        //maybe armour piercing
    }
}
