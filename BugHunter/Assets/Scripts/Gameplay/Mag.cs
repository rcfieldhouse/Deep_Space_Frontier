using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    [SerializeField]private int MagLeft, MagSize=1,AmmoCount=1;
    // Start is called before the first frame update
    void Start()
    {
        MagLeft = MagSize;
    }

    // Update is called once per frame
    //alter mag to subtract a bullet or fill it full on reload 
    public void SetBulletCount()
    {
        if (MagLeft > 0)
            MagLeft--;
    }
    public int getMag()
    {
        return MagLeft;
    }
    public void SetBulletCount(bool var)
    {
        if (var)
        {
            if (AmmoCount > MagSize - MagLeft)
            {
                AmmoCount -= MagSize - MagLeft;
                MagLeft = MagSize;
            }
            else
            {
                MagLeft += AmmoCount;
                AmmoCount = 0;
            }
               
        }
           
    }
}
