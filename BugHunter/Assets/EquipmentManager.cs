using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    IEquip currentEquip;

    public void ChangeEquip(IEquip newEquip)
    {
        if (currentEquip != null)
        {
            currentEquip.Exit(transform.gameObject);
        }
        currentEquip = newEquip;
        currentEquip.Enter(transform.gameObject);
    }

    public int ExecuteEquip(GameObject requester, int damageAmount)
    {
        if (currentEquip != null)
        {
            return currentEquip.Execute(requester, damageAmount);
        }
        else
            return damageAmount;
    }

}