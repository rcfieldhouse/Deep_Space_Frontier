using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "ShopElement", menuName = "Shop/Shop Item",order = 1)]
public class ShopInventory : ScriptableObject
{
    [Header("Details")]
    public string title;
    public string description;

    [Tooltip("Per-material cost of this item")]
    public int[] cost = new int[12];

    [Tooltip("Sprite to be displayed in shop")]
    public Texture2D sprite;

}

[CreateAssetMenu(fileName = "ShopPanel", menuName = "Shop/Shop Panel", order = 2)]
public class ShopTemplate : ScriptableObject
{
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public TMP_Text[] costTxt = new TMP_Text[9];
}
