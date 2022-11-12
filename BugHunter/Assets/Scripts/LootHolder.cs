using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHolder : MonoBehaviour
{
    public static LootHolder instance;
    [SerializeField] List<int> MyStuff;
    //List the types of loot here
    //
    //
    //
    //
    //
    //
    //
    // Start is called before the first frame update
    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        for (int i = 0; i < 6; i++)
        {
            MyStuff.Add(0);
        }
    }
    public void GainLoot(int LootType)
    {
        MyStuff[LootType]++;
    }
}
