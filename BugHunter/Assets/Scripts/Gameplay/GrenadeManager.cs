using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    private Transform Transform;
    public GameObject Grenade,Fruit;
    public GrenadeThrow GrenadeThrow;
    public FruitThrow FruitThrow;
    [SerializeField] private int numGrenades=3;
    [SerializeField] private bool HasFruit=false;
    public int ThrowSelect = 0;
    // Start is called before the first frame update
    void Start()
    {
        GrenadeThrow = GetComponentInChildren<GrenadeThrow>();
        FruitThrow = GetComponentInChildren<FruitThrow>();

        Fruit = FruitThrow.gameObject;
        Grenade = GrenadeThrow.gameObject;

        PlayerInput.Throw += BeginThrow;
        PlayerInput.WeNeedToCookJesse += CookNade;
        PlayerInput.TabThrowable += ChooseThrowable;
        Transform = GrenadeThrow.GetStartPos();
        Grenade.SetActive(false);
        Fruit.SetActive(false);
    }
    public void CookNade() 
    {

        if (ThrowSelect == 0)
            Grenade.SetActive(true);
        else if (ThrowSelect == 1)
            Fruit.SetActive(true);
    }
    public void BeginThrow(Quaternion quaternion)
    {
        if (ThrowSelect == 0)
            BeginThrowGrenade(quaternion);
        else if (ThrowSelect == 1)
            BeginThrowFruit(quaternion);
    }
    public void BeginThrowFruit(Quaternion quaternion)
    {
        if (HasFruit)
        {
            Fruit.SetActive(true);
            FruitThrow.ThrowFruit(quaternion * (Vector3.forward * 25 + Vector3.up * 5));
            HasFruit = false;
        }
    }
    public void BeginThrowGrenade(Quaternion quaternion)
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
    public void ChooseThrowable()
    {
        ThrowSelect++;
        if (ThrowSelect > 1)
            ThrowSelect = 0;
    }
    public void SetHasFruit(bool foo)
    {
        HasFruit = foo;
    }
}
