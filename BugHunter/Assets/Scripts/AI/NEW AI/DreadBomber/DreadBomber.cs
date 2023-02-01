using UnityEngine;

public class DreadBomber : AI
{
    public GameObject Projectile,Orb;
    private int NumDropped = 0;
    [Range(0, 20)] public float ProjectileSpeed = 10;
    // Start is called before the first frame update
    public override void AttackPlayer(GameObject Target)
    {
        if (CanAttack == true && HasAttacked == false)
        {
            Transform MeshLocation = GetComponentInChildren<SkinnedMeshRenderer>().gameObject.transform;
            if (IsSecondaryAttack&&NumDropped<3)
                SecondaryAttack(MeshLocation);
            else
            {
                //play Dante.sound.ogg dread bomber basic shoot attack
                transform.LookAt(Target.transform);
                AI_Animator.SetBool("_IsAttacking", true);
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
            Invoke(nameof(ResetAttacks), 1);
        }
    }
    public void SecondaryAttack(Transform MeshTransform)
    {
        //play Dante.sound.ogg slime spawn
        NumDropped++;
        HasAttacked = true;
        CanAttack = false;
        AI_Animator.SetBool("_IsSpawning", true);
        transform.LookAt(Target.transform);

        Rigidbody rb = Instantiate(Orb, MeshTransform.position-Vector3.up-Vector3.left, Quaternion.identity).GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(transform.rotation * (Vector3.forward + Vector3.up)*5,ForceMode.Impulse);
        Invoke(nameof(ResetAttacks), 1);
    }
    private void ResetAttacks()
    {
        AI_Animator.SetBool("_IsSpawning", false);
        AI_Animator.SetBool("_IsAttacking", false);
    }
}
