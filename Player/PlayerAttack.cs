using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackRange;
    public float AttackSpeed;
    public Action<int> OnDamageChanged;
    public Animator anim;
    public int Counter;
    public bool isWeaponEquipped;
    public bool inFight;
    public int AttackDamage => attackDamage;
    [SerializeField] private int attackDamage;
    private Collider weaponCollider;
    private PlayerMovement playerMovement;
    [SerializeField] private float comboDelay = 2f;
    private float lastClickTime;
    private float timeBetweenAttacks = 0.2f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        AttackSpeed = anim.GetFloat("AttackSpeed");
    }
    public void HandleInput()
    {
        if (isWeaponEquipped)
        {
            Attack();
        }
    }
    public void ChangeDamage(int value)
    {
        attackDamage += value;
        OnDamageChanged?.Invoke(attackDamage);
    }
    public void ChangeDamageTemporary(int time, int value)
    {
        StartCoroutine(ChangeDamageTemporaryC(time, value));
    }
    private IEnumerator ChangeDamageTemporaryC(int time, int value)
    {
        ChangeDamage(value);
        yield return new WaitForSeconds(time);
        ChangeDamage(-value);
    }
    public void ChangeAttackSpeed(float value)
    {
        AttackSpeed += value;
        anim.SetFloat("AttackSpeed", AttackSpeed);
    }
    public void ChangeAttackSpeedTemporary(int time, float value)
    {
        StartCoroutine(ChangeAttackSpeedTemporaryC(time, value));
    }

    private IEnumerator ChangeAttackSpeedTemporaryC(int time, float value)
    {
        ChangeAttackSpeed(value);
        yield return new WaitForSeconds(time);
        ChangeAttackSpeed(-value);
    }
    public void SetWeaponCollider(Collider collider)
    {
        weaponCollider = collider;
    }
    public void ActivateWeaponCollider()
    {
        weaponCollider.enabled = true;
    }
    public void DeactivateWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

    public void Attack()
    {
        if (Time.time - lastClickTime >= comboDelay)
        {
            ResetAttack();
        }

        if (Time.time - lastClickTime >= timeBetweenAttacks)
        {
            anim.SetBool("Running", false);
            playerMovement.agent.isStopped = true;
            playerMovement.agent.ResetPath();
            inFight = true;
            lastClickTime = Time.time;
            anim.SetTrigger("Attack");
        }
    }
    public void DealDamage(Transform target)
    {
        Health enemyHealth = target.GetComponent<Health>();
        enemyHealth.TakeDamage(attackDamage);
    }
    public void ResetAttack()
    {
        inFight = false;
    }
}
