using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidTierQueen : MonoBehaviour
{
    public GameObject Projectile;
    public List<Transform> ProjectileSpawns;
    public GameObject Target;
    [Range(0, 50)] public float ProjectileSpeed = 10;
    [Range(0, -50)] public int AttackDamage = -20;
    [Range(0, 10)] public float AttackSpeed = 5f;
    private void Awake()
    {
        Invoke(nameof(ThornVolley), AttackSpeed);
    }
    void ThornVolley()
    {
        Debug.Log("attacked");
        Target= FindClosestPlayer();
        if (Target != null)
        {
            transform.LookAt(Target.transform);
            for (int i = 0; i < ProjectileSpawns.Count * 3; i++)
            {
                // Debug.Log(i);
                Rigidbody rb = Instantiate(Projectile, ProjectileSpawns[i % 4].position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(Vector3.Normalize(Target.transform.position - (ProjectileSpawns[i % 4].position)) * (ProjectileSpeed + (i * 2)), ForceMode.Impulse);
                rb.gameObject.GetComponent<Thorn>().SetDamage(AttackDamage);
                rb.gameObject.transform.LookAt(Target.transform);
            }
        }

        Invoke(nameof(ThornVolley), AttackSpeed);
    }
    public GameObject FindClosestPlayer()
    {
        GameObject[] AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        float SmallestDistance = 100000.0f;
        GameObject ClosestPlayer = null;
        foreach (GameObject Player in AllPlayers)
        {
            if (Mathf.Abs((Player.transform.position - transform.position).magnitude) < SmallestDistance)
            {
                SmallestDistance = Mathf.Abs((Player.transform.position - transform.position).magnitude);
                ClosestPlayer = Player;
            }
        }
        return ClosestPlayer;
    }
}
