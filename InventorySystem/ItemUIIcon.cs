using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUIIcon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Transform ParentAfterDrag;
    public Image image;
    public Item item;
    public TMP_Text AmountText;
    private static bool dragging;
    public bool Dragable = true;
    public void Initialize(Item item)
    {
        this.item = item;
        image.sprite = item.Sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Dragable) return;

        dragging = true;
        ParentAfterDrag = transform.parent;
        transform.parent = transform.root;
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Dragable) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Dragable) return;
        dragging = false;
        transform.SetParent(ParentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!dragging)
        {
            InventoryUIManager.Instance.OpenDescriptionPanel(item.Description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            InventoryUIManager.Instance.CloseDescriptionPanel();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            PlayerInventory.Instance.currentState.OnUseItem(eventData);
        }
    }
}
