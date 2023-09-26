using UnityEngine;

public class BasicAttackState : IEnemyMovementState
{
    private SkeletonMovement skeleton;
    private Transform playerTransform;
    private Transform enemyTransform;
    private Animator enemyAnimator;
    private PlayerHealth playerHealth;
    private float timeBetweenAttacks;
    private float elapsedTime;
    private float attackRange;

    public BasicAttackState(SkeletonMovement skeleton, Transform playerTransform)
    {
        this.skeleton = skeleton;
        this.playerTransform = playerTransform;
        enemyAnimator = skeleton.GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        enemyTransform = skeleton.transform;
        attackRange = skeleton.attackRange;
        timeBetweenAttacks = skeleton.timeBetweenAttacks;
    }
    public void OnEnter()
    {
        elapsedTime = timeBetweenAttacks;
    }

    public void OnExit()
    {
        enemyAnimator.ResetTrigger("Attack");
    }

    public void OnLogic()
    {
        if(Vector3.Distance(playerTransform.position, enemyTransform.position) >= attackRange)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ChaseState);
        }

        if(elapsedTime >= timeBetweenAttacks)
        {
            enemyTransform.LookAt(playerTransform);
            enemyAnimator.SetTrigger("Attack");
            elapsedTime = 0;
        }

        if (playerHealth.IsDead)
        {
            skeleton.EnemeyMovementStateMachine.ChangeState(skeleton.ChaseOutState);
        }

        elapsedTime += Time.deltaTime;
    }
}
