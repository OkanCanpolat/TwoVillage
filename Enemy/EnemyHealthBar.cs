using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    private Transform cameraToFace;

    private void Awake()
    {
        cameraToFace = Camera.main.transform;

    }

    public void OnDealDamage(float currentHeal, float maxHealth)
    {
        healthBarSlider.value = currentHeal / maxHealth;
    }

    private void LateUpdate()
    {
        transform.LookAt(cameraToFace);
        transform.Rotate(0, 180, 0);
    }
}
