using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface SniperBullet
{
    void WriteName();
   void ShotEffect(GameObject obj);
}
public enum BulletType
{
    Standard, Cryogenic, Incendiary, Electric
}

//shot Effects
internal class Standard : SniperBullet
{
    public void WriteName()
    {
        Debug.Log("Standard");
    }
    public void ShotEffect(GameObject obj)
    {
        //maybe i can do something like does more damage to parts 
        obj.AddComponent<StandardEffect>();
    }
}
internal class Cryogenic : SniperBullet
{
    public void WriteName()
    {
        Debug.Log("Cryogenic");
    }
    public void ShotEffect(GameObject obj)
    {
        obj.AddComponent<CryogenicEffect>();
    }
}
internal class Incendiary : SniperBullet
{
    public void WriteName()
    {
        Debug.Log("Incendiary");
    }
    public void ShotEffect(GameObject obj)
    {
        obj.AddComponent<IncendiaryEffect>();
    }
}
internal class Explosive : SniperBullet
{
    public void WriteName()
    {
        Debug.Log("Explosive");
    }
    public void ShotEffect(GameObject obj)
    {
        obj.AddComponent<ExplosiveEffect>();
    }
}

public class SpecialBulletSelect : MonoBehaviour
{
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
                bullet = new Explosive();
                break;

            default:
                bullet = new Standard();
                break;
        }
        return bullet;
        
    }
   
    // Start is called before the first frame update
    void Awake()
    {
         Bullet = SelectBullet(BulletSelection);
        PlayerInput.UseAbility += ChangeBulletType;
        Bullet.WriteName();
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    public void CallShotEffect(GameObject Object)
    {
        Bullet.ShotEffect(Object);
        
    }
    public void ChangeBulletType()
    {
        BulletSelection += 1;
        if (BulletSelection > BulletType.Electric) BulletSelection = BulletType.Standard;
         Bullet = SelectBullet(BulletSelection);

        Bullet.WriteName();

    }
}


