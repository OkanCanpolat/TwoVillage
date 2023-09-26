using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private float damage;
    [SerializeField] int minGoldReward;
    [SerializeField] int maxGoldReward;
    private Health health;
    private int goldReward;

    private void Awake()
    {
        health = GetComponent<Health>();
        
    }
    private void Start()
    {
        health.OnDying += RewardPlayer;
        goldReward = Random.Range(minGoldReward, maxGoldReward + 1);
    }
    public void DealDamage()
    {
        PlayerStatsContainer.Instance.PHealth.TakeDamageInstanly(damage);
    }
    public void RewardPlayer()
    {
        PlayerInventory.Instance.AddGold(goldReward);
    }

    private void OnDestroy()
    {
        health.OnDying -= RewardPlayer;
        QuestEvents.Instance.OnSkeletonDied?.Invoke();
    }
}
