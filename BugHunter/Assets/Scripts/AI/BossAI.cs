using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    public List<GameObject> AttackColliders; 

    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [Range(0, 10)] [SerializeField] private float SpeedSlider = .5f;

    public float FrontAttackRange, BackAttackRange, SeekRange;
    public Vector3 FrontAttackPos, BackAttackPos;

    private Vector3 WalkVec;

    [Range(0,-50)] public int FrontAttackDamage, BackAttackDamage, AOEAttackDamage;
    public bool PlayerInFrontAttackRange, PlayerInBackAttackRange, PlayerInSeekRange;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<AttackColliders.Count; i++)
        {
            AttackColliders[i].GetComponent<MurderMittens>().SetPlayer(Player);
        }
       animator= GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInFrontAttackRange = Physics.CheckSphere(transform.position + transform.rotation * FrontAttackPos, FrontAttackRange, whatIsPlayer);
        PlayerInBackAttackRange = Physics.CheckSphere(transform.position + transform.rotation * BackAttackPos, BackAttackRange, whatIsPlayer);
        PlayerInSeekRange = Physics.CheckSphere(transform.position , SeekRange, whatIsPlayer);

        PlayerDetection();
        //transform.LookAt(target);
    }
    private void PlayerDetection()
    {
        //depending on the state, do things
        SetAnimsFalse();
        if (PlayerInFrontAttackRange && PlayerInBackAttackRange)
        {
            animator.SetBool("TailH",true);
            StartCoroutine(DoDamage(AOEAttackDamage));
        }
        else if (PlayerInFrontAttackRange)
        {
            animator.SetBool("FrontAttack", true);
            StartCoroutine(DoDamage(FrontAttackDamage));
        }
        else if (PlayerInBackAttackRange)
        {
            animator.SetBool("TailV", true);
         StartCoroutine(DoDamage(BackAttackDamage));
        }
        else if (PlayerInSeekRange)
        {
            Seek();
            animator.SetBool("Walk", true);
        }
    }
    private void Seek()
    {
        WalkVec=(Player.transform.position-transform.position ).normalized;
        Debug.Log(transform.eulerAngles - WalkVec);
        transform.position += 7.5f*WalkVec * Time.deltaTime;
        //  transform.LookAt(Vector3.Lerp(transform.rotation.eulerAngles,Player.transform.position,1));
        transform.LookAt(Player.transform.position);
        transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
    }
    private IEnumerator DoDamage(int Damage)
    {
        for (int i = 0; i < AttackColliders.Count; i++)
        {
            AttackColliders[i].GetComponent<MurderMittens>().SetAttack(true, Damage);
        }
        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < AttackColliders.Count; i++)
        {
            AttackColliders[i].GetComponent<MurderMittens>().SetAttack(false, 0);
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
