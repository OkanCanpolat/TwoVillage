using UnityEngine;

public class OrcAttackState : IEnemyMovementState
{
    private OrcMovement orc;
    private Transform playerTransform;
    private Transform enemyTransform;
    private Animator enemyAnimator;
    private PlayerHealth playerHealth;

    private float timeBetweenAttacks;
    private float elapsedTime;
    private float attackRange;
    private float ultimateTime = 7f;
    private float elapsedUltimeTime;

    public OrcAttackState(OrcMovement orc, Transform playerTransform)
    {
        this.orc = orc;
        this.playerTransform = playerTransform;
        enemyAnimator = orc.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();

        enemyTransform = orc.transform;
        attackRange = orc.attackRange;
        timeBetweenAttacks = orc.timeBetweenAttacks;
    }
    public void OnEnter()
    {
        orc.agent.ResetPath();
        elapsedTime = timeBetweenAttacks;
    }

    public void OnExit()
    {
        enemyAnimator.ResetTrigger("Attack");
    }

    public void OnLogic()
    {
        if (Vector3.Distance(playerTransform.position, enemyTransform.position) >= attackRange)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.ChaseState);
        }

        if(elapsedUltimeTime >= ultimateTime)
        {
            elapsedTime = 0;
            elapsedUltimeTime = 0;
            enemyAnimator.SetTrigger("JumpAttack");
        }

        if (elapsedTime >= timeBetweenAttacks)
        {
            enemyAnimator.SetTrigger("Attack");
            elapsedTime = 0;
        }

        if (playerHealth.IsDead)
        {
            orc.EnemeyMovementStateMachine.ChangeState(orc.ChaseOutState);
        }

        enemyTransform.LookAt(playerTransform);
        elapsedUltimeTime += Time.deltaTime;
        elapsedTime += Time.deltaTime;
    }
}
