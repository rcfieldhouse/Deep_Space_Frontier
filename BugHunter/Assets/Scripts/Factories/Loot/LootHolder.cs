using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LootHolder : MonoBehaviour, IDataPersistence
{
    public List<Loot> Inventory = new List<Loot>{};
    public GameObject Player;
    public GameObject DisplayItemPopup;
    public List<GameObject> Indicators = new List<GameObject> { };
    void Awake()
    {
        //TODO: Instantiate this in the lootholder with the player
        for (int i =0; i <= 12; i++)
        {
            //Inventory.Add(ScriptableObject.CreateInstance<Loot>());
            Inventory.Add(new Loot(0,i));
        }
        if (gameObject.name == "GodOrb")
            return;

        LoadData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
        DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
      
    }
    public int GetLootFromInventory(int index)
    {
        return Inventory[index].Quantity;

    }
    //    foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
    public void GainLoot(int index)
    {
        //SaveData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
        Inventory[index].IncrementLoot(1);
        if (GetComponent<PlayerInput>() != null)
        {
            GameObject Indicator = Instantiate(DisplayItemPopup, transform.parent.GetChild(2));
            Indicators.Add(Indicator);
            StartCoroutine(Remove(Indicator));
            Indicator.GetComponent<DisplayItemPopup>().DisplayNewItem(index);
            foreach (GameObject indicator in Indicators)
            {
                indicator.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition += Vector2.up * 120;
            }
        }
    }
    IEnumerator Remove(GameObject Indicator)
    {
        yield return new WaitForSeconds(2.0f);
        Indicators.Remove(Indicator);
    }
    public void GainLoot(int index, int amount)
    {
        Inventory[index].IncrementLoot(amount);
    }

    public Loot GetInventory(int index)
    {
        return Inventory[index];
    }

    public void LoadData(GameData data)
    {
        if (gameObject.name == "GodOrb")
            return;

        Debug.Log("inventory loaded");
        for (int i = 0; i<Inventory.Count; i++)
        {
            if (i >= 12)
                return;

            Inventory[i].SetQuantity(data.itemQuantity[i]);

        }
    }

    public void SaveData(GameData data)
    {
        if (gameObject.name == "GodOrb")
        {
               for (int i = 0; i < Inventory.Count; i++)
              {
                  if (i >= 12)
                      return;
                Player.GetComponent<LootHolder>().Inventory[i].IncrementLoot(Inventory[i].Quantity);
                Inventory[i].SetQuantity(0);
              }
            return;
        }
       

        Debug.Log("inventory saved");
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (i >= 12)
                return;

            data.itemQuantity[i] = Inventory[i].Quantity;

        }
        
    }
}
