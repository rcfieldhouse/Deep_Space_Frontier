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
    private int[] AmountOwned = new int[3];
    private int[] AmountNeeded = new int[3];
    private int WhichArmour=0;
    [Header("Player Inventory")]
    [SerializeField]
    private LootHolder playerInventory;
    
    //Array of the shops Scriptable Objects
    public ShopInventory[] shopInventory;

    //array for UI panels to coincide with the inventory
    public GameObject[] shopPanelsGO;

    //TODO: store a shopPanel prefab Scriptable Object from the UI
    public ShopTemplate[] shopPanels;

    //array for UI panels to coincide with the inventory
    public Button[] purchaseButtons;
    public EquipmentManager EquipmentManager;
    public GameObject[] CostValues;
    private void Start()
    {

        for (int i = 0; i < shopInventory.Length; i++)
            shopPanelsGO[i].SetActive(true);
      //  LoadShop();
        ValidatePurchasable();
    }
  

    public void LoadShop(int index)
    {
        for (int i=0;i<3; i++)
        {
            AmountOwned[i] = playerInventory.GetLootFromInventory(3 * index + i + 1);
            CostValues[i].GetComponent<TextMeshProUGUI>().text = playerInventory.GetLootFromInventory(3 * index + i+1).ToString();
            CostValues[i].GetComponent<TextMeshProUGUI>().text += "/";
            CostValues[i].GetComponent<TextMeshProUGUI>().text += shopInventory[index].cost[3 * index + i].ToString();
            AmountNeeded[i] = shopInventory[index].cost[3 * index + i];
        }
        WhichArmour = index;
      // for(int i =0; i<shopInventory.Length; i++)
      // {
      //     shopPanels[i].titleTxt.text = shopInventory[i].title;
      //     shopPanels[i].descriptionTxt.text = shopInventory[i].description;
      //
      //     int j = 0;
      //     foreach (Loot loot in playerInventory.Inventory)
      //     {
      //         shopPanels[i].costTxt[j].text = shopInventory[i].cost[j].ToString();
      //     }
      //     
      // }
    }

    //TODO: Make it efficient and cleaner.
    //Not digging the time complexity here, or the readability
    public void Buy()
    {
        for (int i = 0; i < 3; i++)
            if (AmountOwned[i] < AmountNeeded[i])
                return;   
        for (int i = 0; i < 3; i++)
          playerInventory.GetInventory(3*WhichArmour+i+1).DecrementLoot(shopInventory[WhichArmour].cost[3 * WhichArmour + i]);

        PurchaseArmour();
        LoadShop(WhichArmour);
    }
    public void PurchaseArmour()
    {
        switch (WhichArmour)
        {
            case 0:
                EquipmentManager.slime_armor.isEquippable=true;
                break;
            case 1:
                EquipmentManager.TickArmor.isEquippable = true;
                break;
            case 2:
                EquipmentManager.ZephryArmor.isEquippable = true;
                break;
            case 3:
                EquipmentManager.BomberArmor.isEquippable = true;
                break;
        }
            
    }
    public void ValidatePurchasable()
    {
        //for every shop item [i]...
        for (int i = 0; i <shopInventory.Length; i++)
        {
            bool buttonWillBeEnabled = false;

            //Check if player has enough of item [j] for shop item [i]
            for(int j =0; j<playerInventory.Inventory.Count; j++)
            {
                buttonWillBeEnabled = true;
                if (playerInventory.Inventory[j].Quantity < shopInventory[i].cost[j])
                {
                    Debug.LogError("Player has Insufficient Funds! " +
                        "Disabling Button at position: " + i + 
                        "Due to a lack of funds at position: " + j);
                    buttonWillBeEnabled = false;                    
                    break;
                }

                
            }
            if(buttonWillBeEnabled)
            purchaseButtons[i].interactable = true;
        }
    }

    //TODO: Link With Ryan's new Weapon Script
    public void DisplayItemStats()
    {

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
        //LoadShop();
        ValidatePurchasable();
    }


    //[Header("Items")]


}
