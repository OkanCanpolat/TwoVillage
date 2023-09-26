using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public NavMeshAgent agent;
    public Action<float> OnMovementSpeedChanged;
    public float MovementSpeed => movementSpeed;
    [SerializeField] private float lookAtRotationSpeed = 1f;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private Coroutine faceToTarget;
    private RaycastHit hit;
    private const string groundTag = "Ground";
    private Animator animator;
    private float movementSpeed;
    private const float pathEndThreshold = 0.1f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();
        movementSpeed = agent.speed;

        playerHealth.OnPlayerDie += OnPleyerDied;
        playerHealth.OnPlayerRespawn += OnPlayerRespawned;
    }

    void Update()
    {
        if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold)
        {
            animator.SetBool("Running", false);
        }
    }
    public void ControlRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(groundTag))
            {
                if (playerAttack.inFight)
                {
                    animator.ResetTrigger("Attack");
                    playerAttack.inFight = false;
                    playerAttack.DeactivateWeaponCollider();
                }
                agent.isStopped = false;
                agent.SetDestination(hit.point);
                animator.SetBool("Running", true);
            }

            else if (hit.collider.GetComponent<IInteractable>() != null)
            {
                IInteractable target = hit.collider.GetComponent<IInteractable>();
                target.Interact();
            }
        }
    }
    public void ChangeSpeed(float value)
    {
        movementSpeed += value;
        agent.speed = movementSpeed;
        OnMovementSpeedChanged?.Invoke(movementSpeed);
    }
    public void ChangeSpeedTemporary(int time, float value)
    {
        StartCoroutine(ChangeSpeedTemporaryC(time, value));
    }
    private IEnumerator ChangeSpeedTemporaryC(int time, float value)
    {
        ChangeSpeed(value);
        yield return new WaitForSeconds(time);
        ChangeSpeed(-value);
    }
    public void FaceToTargetSlowly(Transform target)
    {
        if (faceToTarget != null)
        {
            StopCoroutine(faceToTarget);
        }

        faceToTarget = StartCoroutine(FaceToTargetSlowlyC(target));
    }
    public IEnumerator FaceToTargetSlowlyC(Transform target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * lookAtRotationSpeed;
            yield return null;
        }
    }
    private void OnPleyerDied()
    {
        agent.ResetPath();
        agent.enabled = false;
    }
    private void OnPlayerRespawned()
    {
        agent.enabled = true;
    }
}
