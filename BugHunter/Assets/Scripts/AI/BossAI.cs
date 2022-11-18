using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    public float FrontAttackRange, BackAttackRange, SeekRange;
    public Vector3 FrontAttackPos, BackAttackPos;
   public bool PlayerInFrontAttackRange, PlayerInBackAttackRange, PlayerInSeekRange;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
       animator= GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInFrontAttackRange = Physics.CheckSphere(transform.position + transform.rotation * FrontAttackPos, FrontAttackRange, whatIsPlayer);
        PlayerInBackAttackRange = Physics.CheckSphere(transform.position + transform.rotation * BackAttackPos, BackAttackRange, whatIsPlayer);
        PlayerInSeekRange = Physics.CheckSphere(transform.position , SeekRange, whatIsPlayer);

        SetAnim();
        //transform.LookAt(target);
    }
    private void SetAnim()
    {
        SetAnimsFalse();
        if (PlayerInFrontAttackRange && PlayerInBackAttackRange)
        {
            animator.SetBool("TailH",true);
        }
        else if (PlayerInFrontAttackRange)
        {
            animator.SetBool("FrontAttack", true);
        }
        else if (PlayerInBackAttackRange)
        {
            animator.SetBool("TailV", true);
        }
        else if (PlayerInSeekRange)
        {
            animator.SetBool("Walk", true);
        }
    }
    private void SetAnimsFalse()
    {
        animator.SetBool("FrontAttack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("TailV", false);
        animator.SetBool("TailH", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+transform.rotation* FrontAttackPos, FrontAttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * BackAttackPos, BackAttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position ,SeekRange);
    }
}
