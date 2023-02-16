using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCannonAmmo : MonoBehaviour
{
    private int Damage = 0;
    private WaitForSeconds Delay = new WaitForSeconds(0.5f);
    Material ArcMaterial;
    // Start is called before the first frame update
    private void Awake()
    {
        ArcMaterial = Resources.Load<Material>("GrenadeExplosion");
        transform.parent = null;
        Invoke(nameof(KillShot), 8.0f);
        StartCoroutine(Enlarge());
    }
    private void OnCollisionEnter(Collision collision)
    { 
        Destroy(gameObject);
    }
    public void SetDamage(int num)
    {
        Damage = num;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
          
            if (other.gameObject.GetComponent<CannonHit>() == null)
            {
                StartCoroutine(ShowLine(other.gameObject.transform));
                other.gameObject.GetComponent<HealthSystem>().ModifyHealth(Damage);
                other.gameObject.AddComponent<CannonHit>();
            }

        }
    }
    private void KillShot()
    {
        Destroy(gameObject);
    }
    private IEnumerator Enlarge()
    {
        yield return Delay;
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
        gameObject.GetComponent<SphereCollider>().radius = 15.0f;
    }


    private IEnumerator ShowLine(Transform Target)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Projectiles/Relic_Cannon_Projectile");
        LineRenderer Line = gameObject.AddComponent<LineRenderer>();
        Line.startWidth = 0.1f;
        Line.endWidth = 0.1f;
        Line.enabled = true;
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, Target.position);
        Line.startColor = Color.cyan;
        Line.endColor = Color.blue;
        Line.material = ArcMaterial;
        yield return new WaitForSeconds(0.075f);
        Destroy(Line);
    }
}
