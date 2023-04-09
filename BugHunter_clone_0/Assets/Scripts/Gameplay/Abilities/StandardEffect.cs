using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEffect : MonoBehaviour
{
    private int Damage;
    bool _IsCrit;
    public void SetValues(Vector3 vec)
    {
        Debug.Log(vec.y);

        Damage = (int)vec.x;
        _IsCrit = false;

        if ((int)vec.y != 0)
        {
            _IsCrit = true;
            float num = Damage * vec.y;
            Damage = (int)num;
        }         
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Invoke(nameof(Effect), 0.05f);
    }
    private void Effect()
    {
        if (GetComponent<DamageIndicator>())
        {
          Transform transform = GetComponent<DamageIndicator>().DamageReceivedFrom;
            gameObject.AddComponent<DamageIndicator>().SetIndicator(transform,Damage, _IsCrit);
        }  
        gameObject.GetComponent<HealthSystem>().ModifyHealth(transform,Damage);
        Destroy(this);
    }
}
