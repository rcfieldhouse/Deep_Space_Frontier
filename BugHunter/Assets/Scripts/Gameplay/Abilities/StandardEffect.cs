using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEffect : MonoBehaviour
{
    private int Damage;
    
    public void SetValues(Vector3 vec)
    {
        Damage = (int)vec.x;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Invoke(nameof(Effect), 0.05f);
    }
    private void Effect()
    {
        gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
        Destroy(this);
    }
}
