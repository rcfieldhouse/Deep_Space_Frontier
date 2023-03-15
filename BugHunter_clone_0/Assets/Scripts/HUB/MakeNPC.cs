using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Merchant, Healer, Blacksmith, Scribe 
}
public class MakeNPC : MonoBehaviour
{
    public NPCType TypeOfVendor;
    private void Awake()
    {
        CreateAVendor(TypeOfVendor);
        Destroy(this);
    }

    public void CreateAVendor(NPCType Type)
    {
    
        switch (Type)
        {
            case NPCType.Merchant:
                gameObject.AddComponent<Merchant>();
                break;
            case NPCType.Healer:
                gameObject.AddComponent<Healer>();
                break;
            case NPCType.Blacksmith:
                gameObject.AddComponent<Blacksmith>();
                break;
            case NPCType.Scribe:
                gameObject.AddComponent<Scribe>();
                break;
            default:
                break;
        }
      
    }
}
