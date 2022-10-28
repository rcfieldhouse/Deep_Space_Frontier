using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    // Start is called before the first frame update
    internal class Assault : ClassInterface
{
        public void CreateClass(GameObject obj)
        {
        obj.AddComponent<HealthSystem>().SetMaxHealth(100);
        obj.AddComponent<GrenadeManager>();
        obj.AddComponent<Dodge>();
        }
    }
    internal class Engineer : ClassInterface
    {
        public void CreateClass(GameObject obj)
        {
        obj.AddComponent<HealthSystem>().SetMaxHealth(200);
         }
    }
    internal class Sniper : ClassInterface
{
        public void CreateClass(GameObject obj)
        {
        obj.AddComponent<HealthSystem>().SetMaxHealth(50);
    }
    }