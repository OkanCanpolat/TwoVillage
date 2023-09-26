using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyMovementState
{
    private SkeletonMovement skeleton;
    private Animator enemyAnimator;
    private Transform playerTransform;
    private Transform enemyTransform;
    private PlayerHealth playerHealth;
    private int timeBetweenPatrols;
    private float time = 0;
    private float aggroRange;

    public IdleState(SkeletonMovement skeleton, Transform playerTransform)
    {
        this.skeleton = skeleton;
        this.playerTransform = playerTransform;
        enemyTransform = skeleton.transform;
        timeBetweenPatrols = skeleton.timeBetweenPatrols;
        enemyAnimator = skeleton.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();

        aggroRange = skeleton.aggroRange;
    }
    public void OnEnter()
    {
        enemyAnimator.SetTrigger("Idle");
    }

    public void OnExit()
    {
        enemyAnimator.ResetTrigger("Idle");
        time = 0;
    }

    public void OnLogic()
    {

        if (Vector3.Distance(enemyTransform.position, playerTransform.position) <= aggroRange &&
            !playerHealth.IsDead)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ChaseState);
        }

        if (time >= timeBetweenPatrols)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ClassicPatrolState);
        }
        time += Time.deltaTime;
    }
}
