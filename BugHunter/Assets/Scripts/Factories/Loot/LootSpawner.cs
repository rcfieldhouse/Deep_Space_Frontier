using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public static LootSpawner instance;
    public GameObject prefab,Drop;
    public List<GameObject> Prefabs;
    public Transform Transform;
    private float num;
    public float spawnForce = 20;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
 

    public void SprayLoot(Transform transform)
    {
        int x = 1;
        num = Random.Range(0.0f, 100.0f);
        LFInterface loot= LootFactory.CreateLoot(LootType.Ammo); 

        if (num < 33.3f)
        {
            x = 0;
            loot = LootFactory.CreateLoot(LootType.Health);
        }
            
        else if (num > 66.7f)
        {
            x = 2;
            loot = LootFactory.CreateLoot(LootType.UpgradeMats);
        }
            

        Create(loot,transform,x);
    }
    void Create(LFInterface foo, Transform transform,int num)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        
        Drop=Instantiate(Prefabs[num], transform.position, Quaternion.identity);
        foo.Create(Drop);

        //apply force on spawn if has rigidbody
        if (rb == null)
            return;

        Vector3 force = (rb.transform.position - this.transform.position).normalized * spawnForce;
        rb.AddForce(force);
    }


   public void DropMaterials(Transform transform, int LootType)
    {
        Drop = Instantiate(Prefabs[2], transform.position, Quaternion.identity);
        MaterialPickup newMat = new MaterialPickup(LootType);
      
        Drop.AddComponent<MaterialPickup>();
        Drop.GetComponent<MaterialPickup>().SetType(LootType);
        //for making it schmoov
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (rb == null)
            return;

        Vector3 force = (rb.transform.position - this.transform.position).normalized * spawnForce;
        rb.AddForce(force);
    }
}
