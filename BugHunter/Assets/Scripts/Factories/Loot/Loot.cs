using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public enum Rarity
    {
        Common,     // 0
        Uncommon,   // 1
        Rare,       // 2
        Epic,       // 3
        Legendary   // 4
    }

    public int quantity;
    public Sprite lootSprite;
    public string lootName;

    public Loot(int amount)
    {
        this.quantity = amount;
    }
    
}
