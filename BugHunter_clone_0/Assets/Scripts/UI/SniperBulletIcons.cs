using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SniperBulletIcons : MonoBehaviour
{
    public List<GameObject> Icons;
    public SniperRifle Sniper;
    private TextMeshProUGUI BulletNum;
    private int BulletType=0;
    // Start is called before the first frame update
    void Awake()
    {
        BulletNum = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        SpecialBulletSelect.NewBulletSelected += SelectBullet;
        for (int i = 0; i < transform.childCount-1; i++)
        {
           Icons.Add(transform.GetChild(i).gameObject);
            Icons[i].SetActive(false);
        }
    }
    private void OnDestroy()
    { 
        SpecialBulletSelect.NewBulletSelected -= SelectBullet;
    }
    private void Update()
    {
        if (BulletType == 0)
            BulletNum.gameObject.SetActive(false);
        else BulletNum.gameObject.SetActive(true);

        BulletNum.text = (Sniper.SpecialBulletCapacity[BulletType]).ToString();
    }
    void SelectBullet(int foo)
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Icons[i].SetActive(false);
        }
        Icons[foo].SetActive(true);
        BulletType = foo;
    }
}
