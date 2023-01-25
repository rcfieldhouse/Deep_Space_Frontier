using UnityEngine;

public class DreadBomber : AI
{
    public GameObject Projectile,Orb;
    [Range(0, 20)] public float ProjectileSpeed = 10;
    // Start is called before the first frame update
    public override void AttackPlayer(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {
            Transform MeshLocation = GetComponentInChildren<SkinnedMeshRenderer>().gameObject.transform;
            if (IsSecondaryAttack)
                SecondaryAttack(MeshLocation);
            else
            {
                transform.LookAt(Target.transform);
              

                Rigidbody rb = Instantiate(Projectile, MeshLocation.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(Vector3.Normalize((Target.transform.position + Vector3.up * Target.GetComponent<CapsuleCollider>().height / 2) - (MeshLocation.position)) * ProjectileSpeed, ForceMode.Impulse);
                rb.gameObject.GetComponent<DreadAmmo>().SetDamage(Attack_1_Damage);
                rb.gameObject.transform.LookAt(Target.transform);
            }

            HasAttacked = true;
            CanAttack = false;
        }
        if (HasAttacked == true)
        {
            HasAttacked = false;
            Invoke(nameof(ResetAttack), Attack_1_Delay);
        }
    }
    public void SecondaryAttack(Transform MeshTransform)
    {
        Debug.Log("Spawned");
        HasAttacked = true;
        CanAttack = false;
        Rigidbody rb = Instantiate(Orb, MeshTransform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(transform.rotation * (Vector3.forward + Vector3.up)*3,ForceMode.Impulse);
     
    }
}
