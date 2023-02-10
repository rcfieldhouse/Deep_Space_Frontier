using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface SniperBullet
{
   void ShotEffect(GameObject obj,Vector3[] vec);
}
public enum BulletType
{
    Standard, Cryogenic, Incendiary, Electric
}

//shot Effects
internal class Standard : SniperBullet
{

    public void ShotEffect(GameObject obj,Vector3[] vec)
    {
        //maybe i can do something like does more damage to parts 
        obj.AddComponent<StandardEffect>().SetValues(vec[0]);
    }
}
internal class Cryogenic : SniperBullet
{
    public void ShotEffect(GameObject obj, Vector3[] vec)
    {
        obj.AddComponent<CryogenicEffect>().SetValues(vec[1]);
    }
}
internal class Incendiary : SniperBullet
{

    public void ShotEffect(GameObject obj, Vector3[] vec)
    {
        obj.AddComponent<IncendiaryEffect>().SetValues(vec[2]);
    }
}
internal class Electric : SniperBullet
{  public void ShotEffect(GameObject obj, Vector3[] vec)
    {
        obj.AddComponent<ElectricEffect>().SetValues(vec[3]);
    }
}

public class SpecialBulletSelect : MonoBehaviour
{
    public static Action<int> NewBulletSelected;
    SniperBullet Bullet;
    [SerializeField] private BulletType BulletSelection=BulletType.Standard;
  
    public SniperBullet SelectBullet(BulletType bulletType)
    {
        SniperBullet bullet;
        switch (bulletType)
        {
            case BulletType.Standard:
                bullet = new Standard();
                break;
            case BulletType.Cryogenic:
                bullet = new Cryogenic();
                break;
            case BulletType.Incendiary:
                bullet = new Incendiary();
                break;
            case BulletType.Electric:
                bullet = new Electric();
                break;

            default:
                bullet = new Standard();
                break;
        }
        return bullet;
        
    }
   public BulletType GetBulletType()
    {
        return BulletSelection;
    }
    // Start is called before the first frame update
    void Awake()
    {
        BulletSelection = BulletType.Standard;
        Bullet = SelectBullet(BulletSelection);
        PlayerInput.UseAbility += ChangeBulletType;       
        Invoke(nameof(BroadcastOnStart), 0.5f);
    }
    private void BroadcastOnStart()
    {
        NewBulletSelected.Invoke((int)BulletSelection);
    }
    private void OnDestroy()
    {
        PlayerInput.UseAbility -= ChangeBulletType;
    }
    // Update is called once per frame

    public void CallShotEffect(GameObject Object,Vector3[] vec)
    {
       
        if (Object.tag!="Player")
        Bullet.ShotEffect(Object,vec);
    }
    public void ChangeBulletType()
    {
      
        BulletSelection += 1;
        if (BulletSelection > BulletType.Electric) BulletSelection = BulletType.Standard;
         Bullet = SelectBullet(BulletSelection);

        NewBulletSelected.Invoke((int)BulletSelection);

    }
}


