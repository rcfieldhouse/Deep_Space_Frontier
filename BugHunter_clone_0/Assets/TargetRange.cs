using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRange : MonoBehaviour
{
    public GameObject Prefab;
    private bool Spawned=false;
    [Range(0, 5)] public float SpawnTimer;
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0&&Spawned==false)
        {
            Invoke(nameof(SpawnNewTarget), SpawnTimer);
            Spawned = true;
        }
    }
    void SpawnNewTarget()
    {
        Instantiate(Prefab, transform);
        Spawned = false;
    }
}
