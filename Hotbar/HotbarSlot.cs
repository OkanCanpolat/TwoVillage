using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (IsEmpty())
        {
            GameObject target = eventData.pointerDrag;
            ItemUIIcon icon = target.GetComponent<ItemUIIcon>();
            if (icon && icon.item.ItemType == ItemType.Potion)
            {
                icon.ParentAfterDrag = transform;
            }
        }
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }
}
