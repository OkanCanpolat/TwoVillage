using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSidePatrolState : IEnemyMovementState
{
    private OrcMovement orc;
    private Animator enemyAnimator;
    private Transform playerTransform;
    private Transform enemyTransform;
    private PlayerHealth playerHealth;
    private Vector3 destination;
    private Vector3 startingPosition;
    private float patrolEndDistance;
    private float aggroRange;
    private float maxXRandom;
    private float maxZRandom;
    private bool patrolEnd;

    public TwoSidePatrolState(OrcMovement orc, Transform playerTransform)
    {
        this.orc = orc;
        this.playerTransform = playerTransform;
        enemyTransform = orc.transform;
        startingPosition = orc.transform.position;
        destination = orc.transform.position;
        patrolEndDistance = orc.agent.stoppingDistance + 0.2f;
        enemyAnimator = orc.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        aggroRange = orc.aggroRange;
        maxXRandom = orc.MaxXRandom;
        maxZRandom = orc.MaxZRandom;
    }
    public void OnEnter()
    {
        if (patrolEnd)
        {
            destination = startingPosition;
            orc.agent.SetDestination(destination);
        }

        else
        {
            destination = GenerateRandomPosition();
            orc.agent.SetDestination(destination);
        }

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
            orc.EnemeyMovementStateMachine.ChangeState(orc.ChaseState);
        }
        if (Vector3.Distance(destination, orc.transform.position) <= patrolEndDistance)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.IdleState);
            patrolEnd = !patrolEnd;
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float y = startingPosition.y;
        float startingx = startingPosition.x;
        float startingz = startingPosition.z;

        float x = Random.Range(-maxXRandom, maxXRandom) + startingx;
        float z = Random.Range(-maxZRandom, maxZRandom) + startingz;
        return new Vector3(x, y, z);
    }

}
