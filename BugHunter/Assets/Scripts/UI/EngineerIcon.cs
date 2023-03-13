using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EngineerIcon : MonoBehaviour
{
    private TextMeshProUGUI TurretNum;
    // Start is called before the first frame update
    void Awake()
    {
        TurretNum = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        TurretAbility.UsedTurret += UseTurret;
    }
    private void OnDestroy()
    {
       
        TurretAbility.UsedTurret -= UseTurret;
    }
    public void UseTurret(int num)
    {
        TurretNum.text =(num).ToString();

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ClearTurrets()
    {
        TurretNum.text = (2).ToString();
    }
  
}
