using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{
    //The object's current health point total
    [SerializeField]
    private HealthSystem Health;
    [SerializeField]
    private GameObject brokenPrefab;
    public bool UseAdditionalUpForce = false;
    public float shatterForce = 10f;
    private float[] randoms = { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f };
    bool Shattered = false;
    public bool _CanDropLoot = true;
    private void Awake()
    {
        Health = GetComponent<HealthSystem>();
        if (Health == null)
            Debug.LogError("There is no Health System attached to " + transform.name);
    }
    private void OnEnable()
    {
        Health.OnObjectDeathT += HandleObjectDeath;
    }
    private void OnDisable()
    {
        Health.OnObjectDeathT -= HandleObjectDeath;
    }
    public void HandleObjectDeath(Transform context)
    {
        if (Shattered == true)
            return;

        Shattered = true;
        //Possibly don't need to check for this breakable tag :3
        foreach (BoxCollider box in gameObject.GetComponents<BoxCollider>())
        {
            box.enabled = false;
        }
         foreach (BoxCollider box in gameObject.GetComponents<BoxCollider>())
        {
            box.enabled = false;
        }
            Debug.Log("Handle Object Death called from " + context.name);
        //create our broken object and reparent it
        GameObject newObject = Instantiate(brokenPrefab,transform.position, transform.rotation);
        newObject.transform.parent = context.transform.parent;

        int index = 0;
        //iterate through children and apply a force
        foreach (Rigidbody rb in newObject.GetComponentsInChildren<Rigidbody>())
            {
           
                //This would normally explode radially, but because the newObject and the context are not in the same position
                //it favors 1 direction
            Vector3 force = (rb.transform.position - context.transform.position).normalized * shatterForce;
           
            float rand = Random.Range(0, 1);

            if (UseAdditionalUpForce == true)
                rb.velocity = Vector3.up *( shatterForce/10.0f)*randoms[index];

            if (index == 9)
                index = 0;

            rb.AddForce(force);
            //if(UseAdditionalUpForce==true)
            //  rb.AddForce(Vector3.up*shatterForce*rand*300);

            rb.gameObject.AddComponent<DissolveRock>();
            index++;
        }
        if (_CanDropLoot == true)
        {
            LootSpawner.instance.SprayLoot(context.transform);
            LootSpawner.instance.SprayLoot(transform);
            LootSpawner.instance.SprayLoot(transform);
            LootSpawner.instance.SprayLoot(transform);
            LootSpawner.instance.SprayLoot(transform);
            LootSpawner.instance.SprayLoot(transform);
        }


        //TODO: Object pooling
        Destroy(gameObject);

    }
}
