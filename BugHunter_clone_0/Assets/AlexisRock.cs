using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexisRock : MonoBehaviour
{
    public ShootableObject TheRock;
    [Range(0, 30)] public float ExplosionTime;
    private void Awake()
    {
        Invoke(nameof(BreakTheRock),ExplosionTime);
    }
    void BreakTheRock()
    {
        TheRock.HandleObjectDeath(TheRock.transform);
    }
}
