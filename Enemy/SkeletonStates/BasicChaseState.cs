using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChaseState : IEnemyMovementState
{

    private SkeletonMovement skeleton;
    private Transform playerTransform;
    private Vector3 startChasePosition;
    private Transform enemyTransform;
    private Animator enemyAnimator;
    private PlayerHealth playerHealth;

    private float maxChaseDistance;
    private float attackRange;
    public BasicChaseState(SkeletonMovement skeleton, Transform playerTransform)
    {
        this.skeleton = skeleton;
        this.playerTransform = playerTransform;
        enemyTransform = skeleton.transform;
        enemyAnimator = skeleton.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();

        maxChaseDistance = skeleton.maxChaseDistance;
        attackRange = skeleton.attackRange;
    }
    public void OnEnter()
    {
        skeleton.agent.SetDestination(playerTransform.position);
        startChasePosition = enemyTransform.position;
        enemyAnimator.SetBool("Walk", true);
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Walk", false);
    }

    public void OnLogic()
    {
        skeleton.agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(enemyTransform.position, playerTransform.position) <= attackRange)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.AttackState);
        }

        if (Vector3.Distance(enemyTransform.position, startChasePosition) >= maxChaseDistance)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ChaseOutState);
        }
        if (playerHealth.IsDead)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ChaseOutState);
        }
    }
    public Vector3 GetChaseStartPosition()
    {
        return startChasePosition;
    }
}
