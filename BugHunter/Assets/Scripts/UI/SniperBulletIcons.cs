using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBulletIcons : MonoBehaviour
{
    public List<GameObject> Icons; 
    // Start is called before the first frame update
    void Awake()
    {
        SpecialBulletSelect.NewBulletSelected += SelectBullet;
        for (int i = 0; i < transform.childCount; i++)
        {
           Icons.Add(transform.GetChild(i).gameObject);
            Icons[i].SetActive(false);
        }
    }
    private void OnDestroy()
    {

        SpecialBulletSelect.NewBulletSelected -= SelectBullet;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SelectBullet(int foo)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Icons[i].SetActive(false);
        }
        Icons[foo].SetActive(true);
    }
}
