using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryNormalState : IInventoryState
{
    public void OnUseItem(PointerEventData eventData)
    {
        GameObject go = eventData.pointerClick;
        ItemUIIcon icon = go.GetComponent<ItemUIIcon>();
        Item item = icon.item;
        item.UseItem();
    }

}
