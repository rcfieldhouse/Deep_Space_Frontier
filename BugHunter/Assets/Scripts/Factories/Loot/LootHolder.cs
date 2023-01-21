using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootHolder : MonoBehaviour, IDataPersistence
{
    [SerializeField] public List<Loot> Inventory = new List<Loot>{};

    void Awake()
    {
        for (int i =0; i < 5; i++)
        {
            Inventory.Add(Loot.CreateInstance<Loot>());
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GainLoot(int Index)
    {
        Inventory[Index].quantity++;
    }

    public void LoadData(GameData data)
    {
        int i = 0;
        foreach (Loot loot in Inventory)
        {
            loot.quantity = data.itemQuantity[i];
            i++;
        }
    }

    public void SaveData(GameData data)
    {
        int i = 0;
        
        foreach(Loot loot in Inventory)
        {
            Debug.Log("Inventory amount is: " + loot.quantity);
            data.itemQuantity[i] = loot.quantity;
            i++;
        }
        
    }
}
