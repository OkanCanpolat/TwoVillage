using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : EnemyMovement
{
    
    public bool DrawGizmos;
    [Header("Agent")]
    public NavMeshAgent agent;

    [Header("Patrol Properties")]
    public float partolRadius = 5f;
    public int timeBetweenPatrols;

    [Header("Chase Properties")]
    public float maxChaseDistance;
    public float aggroRange;

    [Header("Attack Properties")]
    public float timeBetweenAttacks;
    public float attackRange;

    public ClassicPatrolState ClassicPatrolState;
    public IdleState IdleState;
    public BasicChaseState ChaseState;
    public BasicAttackState AttackState;
    public ChaseOutState ChaseOutState;

    private Transform playerTransform;
    private Health enemyHealth;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<Health>();
    }

    private void Start()
    {
        playerTransform = PlayerStatsContainer.Instance.transform;
        ClassicPatrolState = new ClassicPatrolState(this, playerTransform);
        IdleState = new IdleState(this, playerTransform);
        ChaseState = new BasicChaseState(this, playerTransform);
        AttackState = new BasicAttackState(this, playerTransform);
        ChaseOutState = new ChaseOutState(this);

        EnemeyMovementStateMachine = new EnemeyMovementStateMachine(IdleState);
    }

    private void Update()
    {
        if (enemyHealth.isDead) return;
        EnemeyMovementStateMachine.CurrentState.OnLogic();
    }
    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, maxChaseDistance);
        }
    }
}
