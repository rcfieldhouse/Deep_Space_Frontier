using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    private Transform Transform;
    public GameObject Grenade;
    public GrenadeThrow GrenadeThrow; 
    [SerializeField] private int numGrenades=3;
    // Start is called before the first frame update
    void Start()
    {
        GrenadeThrow = Grenade.GetComponent<GrenadeThrow>();
        PlayerInput.UseAbility += BeginThrow;
        Transform = GrenadeThrow.GetStartPos();
    }
    public void BeginThrow(Quaternion quaternion)
    {
      if (numGrenades > 0 && GrenadeThrow.GetIsReady()==true)
        {
            Grenade.SetActive(true);
            StartCoroutine(GrenadeThrow.ThowGrenade(quaternion * (Vector3.forward * 15 + Vector3.up * 5)));
            numGrenades--;
        }
       
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
  
  
  
 
}
