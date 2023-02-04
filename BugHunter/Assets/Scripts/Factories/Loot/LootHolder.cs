using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LootHolder : MonoBehaviour, IDataPersistence
{
    public List<Loot> Inventory = new List<Loot>{};

    void Awake()
    {
        //TODO: Instantiate this in the lootholder with the player
        for (int i =1; i < 10; i++)
        {
            //Inventory.Add(ScriptableObject.CreateInstance<Loot>());
            Inventory.Add(new Loot(0,i));
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GainLoot(int index)
    {
        Inventory[index].IncrementLoot(1);
    }

    public void LoadData(GameData data)
    {
        int i = 0;
        foreach (Loot loot in Inventory)
        {
            loot.SetQuantity(data.itemQuantity[i]);
            i++;
        }
    }

    public void SaveData(GameData data)
    {
        int i = 0;
        foreach(Loot loot in Inventory)
        {
            data.itemQuantity[i] = loot.Quantity;
            i++;
        }
        
    }
}
