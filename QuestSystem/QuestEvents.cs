using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestEvents : MonoBehaviour
{
    public static QuestEvents Instance;
    public UnityAction OnWoodCollected;
    public UnityAction OnSkeletonDied;
    public UnityAction OnOrcDied;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
