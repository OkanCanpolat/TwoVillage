using UnityEngine;

public class WoodEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestEvents.Instance.OnWoodCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
