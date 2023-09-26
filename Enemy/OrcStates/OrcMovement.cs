using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcMovement : EnemyMovement
{
    public bool DrawGizmos;
    [Header("Agent")]
    public NavMeshAgent agent;

    [Header("Patrol Properties")]
    public int timeBetweenPatrols;
    public float MaxXRandom;
    public float MaxZRandom;

    [Header("Chase Properties")]
    public float maxChaseDistance;
    public float aggroRange;

    [Header("Attack Properties")]
    public float timeBetweenAttacks;
    public float attackRange;

    public TwoSidePatrolState PatrolState;
    public OrcIdleState IdleState;
    public OrcChaseState ChaseState;
    public OrcChaseOutState ChaseOutState;
    public OrcAttackState AttackState;

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
        PatrolState = new TwoSidePatrolState(this, playerTransform);
        IdleState = new OrcIdleState(this, playerTransform);
        ChaseState = new OrcChaseState(this, playerTransform);
        AttackState = new OrcAttackState(this, playerTransform);
        ChaseOutState = new OrcChaseOutState(this);

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

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * attackRange));
        }
    }
}
