using System;
using UnityEngine;
using UnityEngine.AI;
public class Health : MonoBehaviour
{
    public bool isDead;
    public Action OnDying;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private EnemyHealthBar healthBar;
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private GameObject healthBarObject;
    private Animator enemyAnimator;
    private NavMeshAgent agent;
    private void Awake()
    {
        currentHealth = maxHealth;
        enemyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.OnDealDamage(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            enemyAnimator.SetTrigger("Die");
            enemyCollider.enabled = false;
            agent.enabled = false;
            healthBarObject.SetActive(false);
            OnDying?.Invoke();
            Invoke("Die", 5);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Enemy thisEnemy = GetComponent<Enemy>();
        thisEnemy.Spawner.RemoveFromList(thisEnemy);
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        healthBar.OnDealDamage(currentHealth, maxHealth);
    }
}
