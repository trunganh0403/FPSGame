using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowTaget : MonoBehaviour
{
    Animator animator;
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected PlayerDamageReceiver playerDamageReceiver;

    public float chaseRange;
    [SerializeField] protected float distanceToTarget;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeDelay = 1f;
    [SerializeField] protected float currenTime = 0f;

    [SerializeField] protected bool isProvoked = false;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isAttack = false;
    [SerializeField] private bool isDelay;


    private void Start()
    {
        chaseRange = 10f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        RanDomSpeed();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget > chaseRange)
        {
            EnemyIdle();
        }
        else
        {
            EngageTarget();
        }
    }

    protected virtual void EngageTarget()
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    public virtual void ChaseTarget()
    {
        if (isDead) return;

        navMeshAgent.isStopped = false;
        isAttack = false;
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdle", false);

        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(target.position);
    }

    protected virtual void AttackTarget()
    {
        navMeshAgent.isStopped = true;
        isAttack = true;
        isDelay = true;
        animator.SetBool("isRunning", false);
        animator.SetTrigger("Attack");
        DameSender();
    }

    public virtual void DameSender()
    {
        if (isAttack == false) return;
        if (!isDelay) return;

        currenTime += Time.fixedDeltaTime;
        if (currenTime < timeDelay) return;
        currenTime = 0f;
        playerDamageReceiver.Deduct(1);
    }

    protected virtual void EnemyIdle()
    {

        navMeshAgent.isStopped = true;
        isAttack = false;
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", true);
    }

    public void Die()
    {
        isDead = true;
        navMeshAgent.isStopped = true; 
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isDead", true);
    }

    protected virtual void RanDomSpeed()
    {
        speed = Random.Range(3f, 4f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyFollowTaget  enemyFollowTaget = other.GetComponent<EnemyFollowTaget>();
            enemyFollowTaget.chaseRange = this.chaseRange;
            Debug.Log(enemyFollowTaget.chaseRange);

        }
    }
}
