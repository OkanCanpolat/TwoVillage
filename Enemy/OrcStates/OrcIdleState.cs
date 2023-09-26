using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcIdleState : IEnemyMovementState
{
    private OrcMovement orc;
    private Animator enemyAnimator;
    private Transform playerTransform;
    private Transform enemyTransform;
    private PlayerHealth playerHealth;

    private int timeBetweenPatrols;
    private float time = 0;
    private float aggroRange;
    public OrcIdleState(OrcMovement orc, Transform playerTransform)
    {
        this.orc = orc;
        this.playerTransform = playerTransform;
        enemyTransform = orc.transform;
        timeBetweenPatrols = orc.timeBetweenPatrols;
        enemyAnimator = orc.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        aggroRange = orc.aggroRange;
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
            orc.EnemeyMovementStateMachine.ChangeState(orc.ChaseState);
        }

        if (time >= timeBetweenPatrols)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.PatrolState);
        }
        time += Time.deltaTime;
    }
}
