using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClassicPatrolState : IEnemyMovementState
{
    private SkeletonMovement skeleton;
    private Animator enemyAnimator;
    private Transform playerTransform;
    private Transform enemyTransform;
    private PlayerHealth playerHealth;

    private Vector3 destination;
    private Vector3 startingPosition;
    private float patrolEndDistance;
    private int patrolCount = 0;
    private int patrolCountBeforeReturnStarting = 3;
    private float patrolRadius;
    private float aggroRange;
    public ClassicPatrolState(SkeletonMovement skeleton, Transform playerTransform)
    {
        this.skeleton = skeleton;
        this.playerTransform = playerTransform;
        enemyTransform = skeleton.transform;
        startingPosition = skeleton.transform.position;
        destination = skeleton.transform.position;
        patrolRadius = skeleton.partolRadius;
        patrolEndDistance = skeleton.agent.stoppingDistance + 0.2f;
        enemyAnimator = skeleton.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        aggroRange = skeleton.aggroRange;
    }
    public void OnEnter()
    {

        SetDestination();
        enemyAnimator.SetBool("Walk", true);
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Walk", false);
    }

    public void OnLogic()
    {
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) <= aggroRange &&
            !playerHealth.IsDead)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ChaseState);
        }
        if (Vector3.Distance(destination, skeleton.transform.position) <= patrolEndDistance)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.IdleState);
        }
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {

        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += skeleton.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = skeleton.transform.position;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    private void SetDestination()
    {
        if (patrolCount >= patrolCountBeforeReturnStarting)
        {
            destination = startingPosition;
            skeleton.agent.SetDestination(destination);
            patrolCount = 0;
        }
        else
        {
            destination = RandomNavmeshLocation(patrolRadius);
            skeleton.agent.SetDestination(destination);
            patrolCount++;
        }
    }

}
