using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>
/// The GameData class stores all of the data we'd like to
/// collect and save on a per player basis
/// </summary>
public class GameData
{
    public int deathCount;
    //Storing complex Data types (such as a list of custom objects)
    //becomes quite troublesome when serializing, to counter this we will
    //only be storing the quantities of each resource.
    //public List<Loot> Inventory;
    public int[] itemQuantity;

    

    // Values found in this constructor represent initial values for a new save-state
    public GameData()
    {
        this.itemQuantity = new int[] { 0, 0, 0, 0, 0 };
        this.deathCount = 0;
    }
}
