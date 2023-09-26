using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttackManager : MonoBehaviour
{
    public Collider UltimateCollider;
    [SerializeField] private float damage;
    public void DealDamage()
    {
        PlayerStatsContainer.Instance.PHealth.TakeDamageInstanly(damage);
    }
    public void ActivateWeapobCollider()
    {
        UltimateCollider.enabled = true;
    }
    public void DeactivateWeapobCollider()
    {
        UltimateCollider.enabled = false;
    }
}
