using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    // Start is called before the first frame update
    internal class Assault : ClassInterface
{
        public void CreateClass(GameObject obj, List<Mesh> Models,List<Material> CharacterMaterials)
        {
        obj.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Models[0];
        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = new Material[3];
        for (var j = 0; j < 3; j++)
        {
            mats[j] = CharacterMaterials[j];
        }
        renderer.materials = mats;


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
    public void CreateClass(GameObject obj, List<Mesh> Models, List<Material> CharacterMaterials)
    {
        obj.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Models[1];

        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = new Material[3];
        for (var j = 0; j < 3; j++)
        {
            mats[j] = CharacterMaterials[j+3];
        }
        renderer.materials = mats;

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
    public void CreateClass(GameObject obj, List<Mesh> Models, List<Material> CharacterMaterials)
    {
        obj.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Models[2];

        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = new Material[3];
        for (var j = 0; j < 3; j++)
        {
            mats[j] = CharacterMaterials[j+6];
        }
        renderer.materials = mats;

        obj.AddComponent<HealthSystem>().SetMaxHealth(50);
        obj.AddComponent<GrenadeManager>();
        obj.AddComponent<SpecialBulletSelect>();
        obj.AddComponent<GunSelect>().SelectGun(obj, 2, 0);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(2).gameObject.SetActive(false);
        obj.GetComponent<ClassCreator>().ClassIcons.transform.GetChild(3).gameObject.SetActive(false);
    }
    }
