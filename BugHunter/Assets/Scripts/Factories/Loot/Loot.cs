using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public enum Rarity
    {
        CommonGrunt,    // 0
        CommonZephyr,   // 1
        CommonSlime,    // 2
        RareGrunt,      // 3
        RareZephyr,     // 4
        RareSlime,      // 5
        EpicGrunt,      // 6
        EpicZephyr,     // 7
        EpicSlime,      // 8
    }

    public int quantity;
    public Sprite lootSprite;
    public string lootName;

    public Loot(int amount)
    {
        this.quantity = amount;
    }
    
}
