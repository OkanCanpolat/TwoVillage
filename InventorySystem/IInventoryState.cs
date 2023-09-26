using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventoryState
{
    public void OnUseItem(PointerEventData eventData);
}
