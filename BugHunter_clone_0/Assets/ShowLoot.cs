using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowLoot : MonoBehaviour
{
    private GameObject Player;
    public int Index=0;
    GameObject FindTheEquipmentLocker(GameObject obj)
    {
        if (obj.GetComponent<EquipmentLockerCanvasManager>() != null)
            return obj;
        else return FindTheEquipmentLocker(obj.transform.parent.gameObject);
    }

    private void Awake()
    {
        Player = FindTheEquipmentLocker(gameObject).transform.parent.parent.GetChild(0).gameObject ;
    }
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = Player.GetComponent<LootHolder>().GetLootFromInventory(Index).ToString();
    }
}
