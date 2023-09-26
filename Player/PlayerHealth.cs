using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action<float, float> OnHealChanged;
    public Action<float> OnMaxHealChanged;
    public Action<int> OnArmourChanged;
    public Action OnPlayerDie;
    public Action OnPlayerRespawn;
    public bool IsDead => isDead;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHeal;
    [SerializeField] private int armour;
    [SerializeField] private float healthGeneretaionPerFiveSecond;
    private Animator animator;
    private Collider playerCollider;
    private const int armourDivisor = 10;
    private const int maxArmour = 500;
    private bool isDead;

    private void Awake()
    {
        currentHeal = maxHealth;
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        StartCoroutine(GenerateHealthPerFiveSecond());
    }
    #region Getters
    public float GetMaximumHealth()
    {
        return maxHealth;
    }
    public int GetArmour()
    {
        return armour;
    }
    #endregion
    public void TakeDamageInstanly(float damage)
    {
        float armourImpactPercantage = (100f - (armour / armourDivisor)) / 100f;
        damage *= armourImpactPercantage;
        currentHeal -= damage;
        OnHealChanged?.Invoke(currentHeal, maxHealth);

        if (currentHeal <= 0 && isDead == false)
        {
            isDead = true;
            playerCollider.enabled = false;
            animator.SetTrigger("Die");
            OnPlayerDie?.Invoke();
        }
    }
    public void RespawnCharacter()
    {
        isDead = false;
        playerCollider.enabled = true;
        animator.SetTrigger("Respawn");
        OnPlayerRespawn?.Invoke();
        HealInstantly(maxHealth);
    }
    public void TakeDamageInTime(int time, float damage)
    {
        StartCoroutine(DamageInTimeCor(time, damage));
    }
    private IEnumerator DamageInTimeCor(int time, float damage)
    {
        int sec = 0;
        float damagePerSecond = damage / time;

        while (sec < time)
        {
            yield return new WaitForSeconds(1);
            TakeDamageInstanly(damagePerSecond);
            sec++;
        }
    }
    public void HealInstantly(float heal)
    {
        currentHeal += heal;
        currentHeal = Mathf.Clamp(currentHeal, 0, maxHealth);
        OnHealChanged?.Invoke(currentHeal, maxHealth);
    }
    public void HealInTime(int time, float heal)
    {
        StartCoroutine(HealInTimeCor(time, heal));
    }
    private IEnumerator HealInTimeCor(int time, float heal)
    {
        int sec = 0;
        float healPerSecond = heal / time;

        while (sec < time)
        {
            yield return new WaitForSeconds(1);
            HealInstantly(healPerSecond);
            sec++;
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        OnMaxHealChanged?.Invoke(maxHealth);
        OnHealChanged?.Invoke(currentHeal, maxHealth);
    }
    public void DecreaseMaxHealth(float amount)
    {
        maxHealth -= amount;
        OnMaxHealChanged?.Invoke(maxHealth);
        OnHealChanged?.Invoke(currentHeal, maxHealth);
    }
    public void ChangeArmour(int value)
    {
        armour += value;
        armour = Mathf.Clamp(armour, 0, maxArmour);
        OnArmourChanged?.Invoke(armour);
    }
    public void ChangeArmourTemporary(int time, int value)
    {
        StartCoroutine(ChangeArmourTemporaryC(time, value));
    }
    private IEnumerator ChangeArmourTemporaryC(int time, int value)
    {
        ChangeArmour(value);
        yield return new WaitForSeconds(time);
        ChangeArmour(-value);
    }
    public void ChangeHealthGenerationRate(float value)
    {
        healthGeneretaionPerFiveSecond += value;
    }
    public void ChangeHealthGenerationRateTeporary(int time, float value)
    {
        StartCoroutine(ChangeHealthGenerationRateTeporaryC(time, value));
    }
    private IEnumerator ChangeHealthGenerationRateTeporaryC(int time, float value)
    {
        ChangeHealthGenerationRate(value);
        yield return new WaitForSeconds(time);
        ChangeHealthGenerationRate(-value);
    }
    private IEnumerator GenerateHealthPerFiveSecond()
    {
        WaitForSeconds s = new WaitForSeconds(5f);

        while (true)
        {
            HealInstantly(healthGeneretaionPerFiveSecond);
            yield return s;
        }
    }
    
}
