using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    public Transform StartingTransform;
    public GameObject Grenade,GrenadeGraphic,Fruit,WeaponCamera,PlayerCamera;
    public FruitThrow FruitThrow;
    public Quaternion quat;
    private PreviewThrow PreviewThrow;
    public Vector3 ThrowForce = (Vector3.forward * 25 + Vector3.up * 5);
    [SerializeField] private int numGrenades=3;
    [SerializeField] private bool HasFruit=false,_CanThrow=true;
    public int ThrowSelect = 0;
    // Start is called before the first frame update

    void Awake()
    {

        FruitThrow = GetComponentInChildren<FruitThrow>();
        PreviewThrow = transform.parent.GetChild(1).gameObject.AddComponent<PreviewThrow>();

        Fruit = FruitThrow.gameObject;
        Grenade = Resources.Load<GameObject>("grenade");
        GrenadeGraphic = GetComponentInChildren<GrenadeThrow>().gameObject;
        GrenadeGraphic.SetActive(false);
        PlayerCamera = transform.parent.GetComponentInChildren<FollowWeaponCam>().gameObject;
        WeaponCamera = transform.parent.GetComponentInChildren<Recoil>().gameObject;
        PlayerInput.Throw += BeginThrow;
        PlayerInput.WeNeedToCookJesse += CookNade;
        PlayerInput.TabThrowable += ChooseThrowable;

        PlayerCamera.SetActive(false);
        Grenade.SetActive(false);
        Fruit.SetActive(false);
    }
    private void OnDestroy()
    {
        PlayerInput.Throw -= BeginThrow;
        PlayerInput.WeNeedToCookJesse -= CookNade;
        PlayerInput.TabThrowable -= ChooseThrowable;
    }
    public void CookNade() 
    {
        if (_CanThrow == false)
            return;

        if (ThrowSelect == 0 && numGrenades > 0)
        {
            GrenadeGraphic.SetActive(true);
            PreviewThrow.CookNade();
            Grenade.SetActive(true);
        }
           
        else if (ThrowSelect == 1&& HasFruit == true)
        {
            PreviewThrow.CookNade();
            Fruit.SetActive(true);
        }
        WeaponCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
    }
    public void BeginThrow(Quaternion quaternion)
    {
        if (_CanThrow == false)
            return;

        if (ThrowSelect == 0 && numGrenades > 0)
        {
            BeginThrowGrenade(quaternion);
            PreviewThrow.Release();
        }
          
        else if (ThrowSelect == 1)
        {
            BeginThrowFruit(quaternion);
            PreviewThrow.Release();
        }
        _CanThrow = false;
        transform.parent.GetComponentInChildren<ThrowableSwap>().DisplayInfo();
    }
    public int GetNumNades()
    {
        return numGrenades;
    }
    public void BeginThrowFruit(Quaternion quaternion)
    {
        if (HasFruit)
        {
            Fruit.SetActive(true);
            FruitThrow.ThrowFruit(quaternion * ThrowForce,StartingTransform);
            HasFruit = false;
        }
    }
    public void BeginThrowGrenade(Quaternion quaternion)
    {
      if (numGrenades > 0)
        {
            numGrenades--;
            quat = quaternion;
          
            Invoke(nameof(ThrowDelay),0.65f);
       
        }     
    }
    public void ThrowDelay()
    {
        PlayerCamera.SetActive(false);
        GrenadeGraphic.SetActive(false);
        GameObject nade = Instantiate(Grenade,WeaponCamera.transform.position - Vector3.up / 4, Quaternion.identity);
        nade.GetComponent<GrenadeThrow>().ThowGrenade(quat * ThrowForce);
        Invoke(nameof(EnableCam),1.5f);
    }
    void EnableCam()
    {
        WeaponCamera.SetActive(true);
        PlayerCamera.SetActive(false);

        if(numGrenades>0)
        _CanThrow = true;
    }
    // Update is called once per frame
    public void GainGrenades()
    {
        numGrenades++;
    }
   public void GainGrenades(int num)
    {
        _CanThrow = true;
        if (numGrenades > 4)
            return;

        numGrenades += num;
    }
    public void SetGrenades(int num)
    {
        numGrenades = num;
    }
    public void ChooseThrowable()
    {
        ThrowSelect++;
        if (ThrowSelect > 1)
            ThrowSelect = 0;
    }
    public bool GetHasFruit()
    {
        return HasFruit;
    }
    public Transform GetFruitStart()
    {
        return StartingTransform;
    }
    public void SetHasFruit(bool foo,Transform transform)
    {
        if (foo == true)
        {
            StartingTransform = transform;
        }
        HasFruit = foo;
    }
}
