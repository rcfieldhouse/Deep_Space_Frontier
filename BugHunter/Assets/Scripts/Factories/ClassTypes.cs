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
        obj.AddComponent<GunSelect>().SelectGun(obj,5,4);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(4).gameObject.SetActive(false);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
    internal class Engineer : ClassInterface
    {
        public void CreateClass(GameObject obj)
        {
        obj.AddComponent<GrenadeManager>();
        obj.AddComponent<HealthSystem>().SetMaxHealth(200);
        obj.AddComponent<TurretAbility>();
        obj.AddComponent<GunSelect>().SelectGun(obj, 1, 6);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(2).gameObject.SetActive(false);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(4).gameObject.SetActive(false);
    }
    }
    internal class Sniper : ClassInterface
    {
        public void CreateClass(GameObject obj)
        {
        obj.AddComponent<HealthSystem>().SetMaxHealth(50);
        obj.AddComponent<GrenadeManager>();
        obj.AddComponent<SpecialBulletSelect>();
        obj.AddComponent<GunSelect>().SelectGun(obj, 2, 0);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(2).gameObject.SetActive(false);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(3).gameObject.SetActive(false);
    }
    }
