using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffIndicatorIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image ItemIcon;
    [SerializeField] private Image TimeImage;
    private int buffTime;
    private string buffDescription;
    private float elapsedTime;
    public void Init(int time, string description, Sprite icon)
    {
        buffTime = time;
        buffDescription = description;

        ItemIcon.sprite = icon;
        StartCoroutine(StartTimer(buffTime));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BuffIndicatorUIManager.Instance.OpenDescriptionPanel(buffDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BuffIndicatorUIManager.Instance.CloseDescriptionPanel();
    }

    private IEnumerator StartTimer(int time)
    {
        while(elapsedTime < time)
        {
            TimeImage.fillAmount = elapsedTime / time;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        BuffIndicatorUIManager.Instance.CloseDescriptionPanel();
        Destroy(gameObject);
    }

}
