using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemy : MonoBehaviour
{
    private void Awake()
    {
        ArenaManager.instance.NumEnemies++;
    }
    private void OnDestroy()
    {
        ArenaManager.instance.NumEnemies--;
    }
}
