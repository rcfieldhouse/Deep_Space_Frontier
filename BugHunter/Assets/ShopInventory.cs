using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "ShopElement", menuName = "Shop/Weapon Upgrade",order = 1)]
public class ShopInventory : MonoBehaviour
{
    [Header("Details")]
    public string title;
    public string description;
    [Tooltip("Per-material cost of this item")]
    public int[] cost;
    [Tooltip("Sprite to be displayed in shop")]
    public Texture2D sprite;
}

[CreateAssetMenu(fileName = "ShopPanel", menuName = "Shop/Shop Panel", order = 2)]
public class ShopTemplate : ScriptableObject
{
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public TMP_Text[] costTxt;
}
