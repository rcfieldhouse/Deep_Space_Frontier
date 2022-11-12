using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHolder : MonoBehaviour
{
    public static LootHolder instance;
    [SerializeField] int MyStuff = 0;

    // Start is called before the first frame update
    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void GainLoot()
    {
        MyStuff++;
    }
}
