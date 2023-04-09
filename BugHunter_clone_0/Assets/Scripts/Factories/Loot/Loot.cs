using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Loot", menuName = "Loot Item/Create New Item")]
[System.Serializable]
public class Loot
{
    [SerializeField]
    private MonsterLoot lootType = MonsterLoot.None;
    [SerializeField]
    private int quantity = 0;
    [SerializeField]
    private Sprite lootSprite;
    [SerializeField]
    private string lootName = "New Loot";

    //these are Read-Only getters built for type safety
    public string Name => lootName;
    public int Quantity => quantity;
    public Sprite Sprite => lootSprite;
    public MonsterLoot Type => lootType;


    public Loot(int amount, int type)
    {
        this.lootName = "New Loot";
        this.quantity = amount;
        this.lootType = (MonsterLoot)type;
    }

    public void IncrementLoot(int increment)
    {
        quantity += increment;
    }
    public void DecrementLoot(int decrement)
    {
        quantity -= decrement;
    }
    public void SetQuantity(int newQuantity)
    {
        quantity = newQuantity;
    }
}


