using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    private Transform Transform;
    public GrenadeThrow GrenadeThrow; 
    private int numGrenades=3;
    // Start is called before the first frame update
    void Start()
    {
        GrenadeThrow = GetComponentInChildren<GrenadeThrow>();
        Transform = GrenadeThrow.GetStartPos();
    }

    // Update is called once per frame
  public void GainGrenades()
    {
        numGrenades++;
    }
   public void GainGrenades(int num)
    {
        numGrenades += num;
    }
  
   public int GetNumGrenades()
    {
        return numGrenades;
    }
    public void ThrowGrenade()
    {
        numGrenades--;
    }
 
}
