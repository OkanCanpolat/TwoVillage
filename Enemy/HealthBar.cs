using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth Health;
    public int targetSecond = 1;
    [SerializeField] private Slider healthBarSlider;
    private float targetHealthLevel = 1f;

    private void Awake()
    {
        Health.OnHealChanged += UpdateHealthBar;
    }
    public void UpdateHealthBar(float currentHeal, float maxHealth)
    {
        StartCoroutine(UpdateBar(currentHeal, maxHealth));
    }
    public IEnumerator UpdateBar(float currentHeal, float maxHealth)
    {
        targetHealthLevel = currentHeal / maxHealth;
        float elapsedTime = 0f;

        while (elapsedTime < targetSecond)
        {
            healthBarSlider.value =
                Mathf.MoveTowards(healthBarSlider.value, targetHealthLevel, targetSecond * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        healthBarSlider.value = targetHealthLevel;
    }

}
