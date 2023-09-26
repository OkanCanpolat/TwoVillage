using UnityEngine;

public class OrcChaseState : IEnemyMovementState
{
    private OrcMovement orc;
    private Transform playerTransform;
    private Vector3 startChasePosition;
    private Transform enemyTransform;
    private Animator enemyAnimator;
    private PlayerHealth playerHealth;

    private float maxChaseDistance;
    private float attackRange;
    public OrcChaseState(OrcMovement orc, Transform playerTransform)
    {
        this.orc = orc;
        this.playerTransform = playerTransform;
        enemyTransform = orc.transform;
        enemyAnimator = orc.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();

        maxChaseDistance = orc.maxChaseDistance;
        attackRange = orc.attackRange;
    }
    public void OnEnter()
    {
        orc.agent.SetDestination(playerTransform.position);
        startChasePosition = enemyTransform.position;
        enemyAnimator.SetBool("Walk", true);
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Walk", false);
    }

    public void OnLogic()
    {
        orc.agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(enemyTransform.position, playerTransform.position) <= attackRange)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.AttackState);
        }

        if (Vector3.Distance(enemyTransform.position, startChasePosition) >= maxChaseDistance)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.ChaseOutState);
        }
        if (playerHealth.IsDead)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.ChaseOutState);
        }
    }
    public Vector3 GetChaseStartPosition()
    {
        return startChasePosition;
    }


}
