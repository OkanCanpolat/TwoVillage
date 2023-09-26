using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsContainer : MonoBehaviour
{
    public static PlayerStatsContainer Instance;
    public PlayerMovement PMovement;
    public PlayerHealth PHealth;
    public PlayerAttack PAttack;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
