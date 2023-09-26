using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryShopState : IInventoryState
{
    public void OnUseItem(PointerEventData eventData)
    {
        GameObject go = eventData.pointerClick;
        ItemUIIcon icon = go.GetComponent<ItemUIIcon>();
        if (ShopManager.Instance.CurrentWindow.Add(icon.item))
        {
            PlayerInventory.Instance.RemoveItem(icon.item);
        }
    }

   
}
