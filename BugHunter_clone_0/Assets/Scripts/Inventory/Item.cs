using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script creates scriptable objects that can be made and assigned an id, a name, a value, and an icon

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory Item/Create New Item")]
public class Item: ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon; 
}
