using UnityEngine;
using UnityEngine.AI;

public class ChaseOutState : IEnemyMovementState
{
    private SkeletonMovement skeleton;
    private Transform enemyTransform;
    private Vector3 startingPosition;
    private Animator enemyAnimator;
    private Health health;
    private float speedBonus = 5f;

    public ChaseOutState(SkeletonMovement skeleton)
    {
        this.skeleton = skeleton;
        enemyTransform = skeleton.transform;
        enemyAnimator = skeleton.GetComponent<Animator>();
        health = skeleton.GetComponent<Health>();
    }
    public void OnEnter()
    {
        startingPosition = skeleton.ChaseState.GetChaseStartPosition();
        enemyAnimator.SetBool("Walk", true);
        skeleton.agent.SetDestination(startingPosition);
        health.RestoreFullHealth();
        skeleton.agent.speed += speedBonus;
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Walk", false);
        skeleton.agent.speed -= speedBonus;
    }

    public void OnLogic()
    {
        if(Vector3.Distance(enemyTransform.position, startingPosition) <= 1.2f)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.IdleState);
        }
    }
}
