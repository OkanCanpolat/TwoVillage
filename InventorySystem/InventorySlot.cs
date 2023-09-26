using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (IsEmpty())
        {
            GameObject target = eventData.pointerDrag;
            ItemUIIcon icon = target.GetComponent<ItemUIIcon>();
            if (icon)
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
