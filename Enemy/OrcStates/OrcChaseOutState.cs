using UnityEngine;

public class OrcChaseOutState : IEnemyMovementState
{
    private OrcMovement orc;
    private Transform enemyTransform;
    private Vector3 startingPosition;
    private Animator enemyAnimator;
    private Health health;
    private float speedBonus = 3f;

    public OrcChaseOutState(OrcMovement orc)
    {
        this.orc = orc;
        enemyTransform = orc.transform;
        enemyAnimator = orc.GetComponent<Animator>();
        health = orc.GetComponent<Health>();
    }
    public void OnEnter()
    {
        startingPosition = orc.ChaseState.GetChaseStartPosition();
        enemyAnimator.SetBool("Walk", true);
        orc.agent.SetDestination(startingPosition);
        health.RestoreFullHealth();
        orc.agent.speed += speedBonus;
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Walk", false);
        orc.agent.speed -= speedBonus;
    }

    public void OnLogic()
    {
        if (Vector3.Distance(enemyTransform.position, startingPosition) <= 1.2f)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.IdleState);
        }
    }
}
