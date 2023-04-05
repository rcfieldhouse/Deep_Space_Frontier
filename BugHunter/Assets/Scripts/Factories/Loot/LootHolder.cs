using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LootHolder : MonoBehaviour, IDataPersistence
{
    public List<Loot> Inventory = new List<Loot>{};
    public GameObject Player;
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

    public void GainLoot(int index)
    {
        SaveData(GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().gameData);
        Inventory[index].IncrementLoot(1);
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
