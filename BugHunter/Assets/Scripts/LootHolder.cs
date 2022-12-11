using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
      void Start()
      { 
        DontDestroyOnLoad(gameObject);
      }
// Start is called before the first frame update

    private void Awake()
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
