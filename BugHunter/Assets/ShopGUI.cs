using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.AttributeUsage(System.AttributeTargets.Field,
    AllowMultiple = true)]
public class SeperatorAttribute : PropertyAttribute
{
    public readonly float Height;
    public readonly float Spacing;

    public SeperatorAttribute(float height = 1, float spacing = 10)
    {
        Height = height;
        Spacing = spacing;
    }
}

public class ShopGUI : MonoBehaviour
{
    [Header("Player Inventory")]
    [SerializeField]
    private LootHolder playerInventory;

    //Array of the shops Scriptable Objects
    private ShopInventory[] shopInventory;

    //array for UI panels to coincide with the inventory
    private GameObject[] shopPanelsGO;

    //TODO: store a shopPanel prefab from the UI
    private ShopTemplate[] shopPanels;

    //array for UI panels to coincide with the inventory
    private Button[] purchaseButtons;

    private void Start()
    {
        for (int i = 0; i < shopInventory.Length; i++)
            shopPanelsGO[i].SetActive(true);
        LoadShop();
        ValidatePurchasable();

    }


    public void LoadShop()
    {
        for(int i =0; i<shopInventory.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopInventory[i].title;
            shopPanels[i].descriptionTxt.text = shopInventory[i].description;

            int j = 0;
            foreach (Loot loot in playerInventory.Inventory)
            {
                shopPanels[i].costTxt[j].text = shopInventory[i].cost[j].ToString();
            }
            
        }
    }

    public void ValidatePurchasable()
    {

    }
    //TODO: Verify that the values of i are correspondant with the Loot Variables
    public void PurchaseItem(int itemNumber)
    {
        int i = 0;
        foreach (Loot loot in playerInventory.Inventory)
        {
            if(loot.Quantity >= shopInventory[itemNumber].cost[i])
            {
                loot.DecrementLoot(shopInventory[itemNumber].cost[i]);
            }
            else
            {
                Debug.LogError("Player has Insufficient Funds! " +
                    "This should not be possible unless there is an error " +
                    "with the ValidatePurchasable() function");
            }
            i++;
        }
     
    }

    /// <summary>
    /// Set's all of the players resources to 99 for testing.
    /// </summary>
    public void Cheat()
    {
        foreach(Loot loot in playerInventory.Inventory)
        {
            loot.SetQuantity(99);
        }
        ValidatePurchasable();
    }


    //[Header("Items")]


}
