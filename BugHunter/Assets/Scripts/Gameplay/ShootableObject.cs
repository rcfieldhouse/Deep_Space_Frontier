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

    public float shatterForce = 10f;

    private void Awake()
    {
        Health = GetComponent<HealthSystem>();
        if (Health == null)
            Debug.LogError("There is no Health System attached to " + transform.name);
    }
    private void OnEnable()
    {
        Health.OnObjectDeath += HandleObjectDeath;
    }
    private void OnDisable()
    {
        Health.OnObjectDeath -= HandleObjectDeath;
    }
    public void HandleObjectDeath(GameObject context)
    {
        //Possibly don't need to check for this breakable tag :3

        Debug.Log("Handle Object Death called from " + context.name);
        //create our broken object and reparent it
        GameObject newObject = Instantiate(brokenPrefab,transform.position, transform.rotation);
        newObject.transform.parent = context.transform.parent;
        LootSpawner.instance.SprayLoot(context.transform);
        LootSpawner.instance.SprayLoot(transform);
        LootSpawner.instance.SprayLoot(transform);
        LootSpawner.instance.SprayLoot(transform);
        LootSpawner.instance.SprayLoot(transform);
        LootSpawner.instance.SprayLoot(transform);


        //iterate through children and apply a force
        foreach (Rigidbody rb in newObject.GetComponentsInChildren<Rigidbody>())
            {
                //This would normally explode radially, but because the newObject and the context are not in the same position
                //it favors 1 direction
                Vector3 force = (rb.transform.position - context.transform.position).normalized * shatterForce;
                rb.AddForce(force);
            rb.gameObject.AddComponent<DissolveRock>();

                //TODO: apply a coroutine to delete the pieces after some time expires
            }
            //TODO: Object pooling
            Destroy(context);

    }
}
