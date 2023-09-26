using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public EnemySpawner Spawner;

    public void Interact()
    {
        PlayerMovement.Instance.FaceToTargetSlowly(transform);
    }
}
