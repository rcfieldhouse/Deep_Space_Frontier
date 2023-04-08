using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItemPopup : MonoBehaviour
{

    public List<Sprite> sprites = new List<Sprite>();
    public List<string> Names = new List<string>();

    public Image Itemimage;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemRarity;
    public Animator popupAnim;

    //.GetComponent<TextMeshProUGUI>().text
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayNewItem(int index)
    {
        Itemimage.sprite = sprites[index - 1];
        ItemName.text = Names[index - 1];

        switch (index % 3)
        {
            case 0: ItemRarity.text = "Rare";
                break;
            case 1: ItemRarity.text = "Common";
                break;
            case 2: ItemRarity.text = "Uncommon";
                break;
        }
        popupAnim.Play("ItemPopupAnim");
    }
}
