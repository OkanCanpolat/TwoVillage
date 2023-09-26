using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    public static HotbarController Instance;
    public List<HotbarSlot> HotbarSlots;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void UseHotbar(int index)
    {
        HotbarSlot pressed = HotbarSlots[index];
        if (!pressed.IsEmpty())
        {
            Transform itemTransform = pressed.transform.GetChild(0);
            ItemUIIcon icon = itemTransform.GetComponent<ItemUIIcon>();
            icon.item.UseItem();
        }
    }
}
