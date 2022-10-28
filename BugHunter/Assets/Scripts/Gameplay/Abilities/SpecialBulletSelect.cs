using System.Collections;
using System.Collections.Generic;
using UnityEngine;
internal interface SniperBullet
{
   void ShotEffect();
}
internal enum BulletType
{
    Standard, Cryogenic, Incendiary, Electric
}

//shot Effects
internal class Standard : SniperBullet
{
    public void ShotEffect()
    {
        Debug.Log("Standard");
    }
}
internal class Cryogenic : SniperBullet
{
    public void ShotEffect()
    {

    }
}
internal class Incendiary : SniperBullet
{
    public void ShotEffect()
    {

    }
}
internal class Electric : SniperBullet
{
    public void ShotEffect()
    {

    }
}

internal class SpecialBulletSelect : MonoBehaviour
{
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
   
    // Start is called before the first frame update
    void Awake()
    {
        SniperBullet Bullet = SelectBullet(BulletSelection);
        Bullet.ShotEffect();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


