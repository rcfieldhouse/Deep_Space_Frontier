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
    public Image Background;
    public Animator popupAnim;
    private float num = 0.0f;
    //.GetComponent<TextMeshProUGUI>().text

    public void DisplayNewItem(int index)
    {
        Itemimage.sprite = sprites[index - 1];
        ItemName.text = Names[index - 1];

        switch (index % 3)
        {
            case 0: ItemRarity.text = "Rare";
                Background.color = Color.cyan;
                break;           
            case 1: ItemRarity.text = "Common";
                Background.color = Color.white;
                break;
            case 2: ItemRarity.text = "Uncommon";
                Background.color = Color.green;
                break;
        }
        StartCoroutine(FadeOut());
        popupAnim.SetBool("Animate", true);
    }
    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.0f);
        while (num < 1.0f)
        {
            //Debug.Log(Vector3.zero + (offset + num) * Vector3.up);
            num += 0.02f;
                yield return new WaitForSeconds(0.02f);
            ItemName.alpha = 1.0f - num;
            ItemRarity.alpha = 1.0f - num;
            var tempColor = Background.GetComponent<Image>().color;
            tempColor.a = 1 - num;
            Background.color = tempColor;

            var tempColor2 = Itemimage.GetComponent<Image>().color;
            tempColor2.a = 1 - num;
            Itemimage.color = tempColor2;
        }
        Destroy(gameObject);
    }

}
