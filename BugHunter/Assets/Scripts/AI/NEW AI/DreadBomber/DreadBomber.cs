using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreadBomber : AI
{
    public GameObject Projectile;
    [Range(0, 20)] public float ProjectileSpeed = 10;
    // Start is called before the first frame update
    public override void AttackPlayer(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {
            transform.LookAt(Target.transform);
            Transform MeshLocation = GetComponentInChildren<MeshRenderer>().gameObject.transform;

            Rigidbody rb = Instantiate(Projectile, MeshLocation.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(Vector3.Normalize(Target.transform.position - (MeshLocation.position)) * ProjectileSpeed, ForceMode.Impulse);
            rb.gameObject.GetComponent<DreadAmmo>().SetDamage(Attack_1_Damage);
            rb.gameObject.transform.LookAt(Target.transform);

            HasAttacked = true;
            CanAttack = false;
        }
        if (HasAttacked == true)
        {
            HasAttacked = false;
            Invoke(nameof(ResetAttack), Attack_1_Delay);
        }
    }
}
