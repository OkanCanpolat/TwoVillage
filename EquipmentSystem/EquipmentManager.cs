using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    public List<EquipmentSlot> Slots;
    public List<GameObject> EquipableItems;
    public Action<IEquipment, Item> OnItemEquipped;
    public Action<IEquipment, InventorySlot> OnItemUnEquipped;
    public Action<IEquipment, Item> OnItemChanged;
    public Action OnEquipmentWindowStateChange;

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
    public EquipmentSlot GetSlot(EquipmentType type)
    {
        foreach(EquipmentSlot slot in Slots)
        {
            if(slot.Type == type)
            {
                return slot;
            }
        }

        return Slots[0];
    }

    public void Equip(IEquipment equipment, Item item)
    {
        EquipmentSlot targetSlot = GetSlot(equipment.Type);

        if (targetSlot.IsEmpty())
        {
            equipment.OnEquip();
            WearEquipment(equipment.ItemIndex);
            OnItemEquipped?.Invoke(equipment, item);
        }
        else
        {
            ChangeItem(equipment, targetSlot, item);
        }
    }

    public void UnEquip(IEquipment equipment)
    {
        InventorySlot targetSlot = null; 

        if (PlayerInventory.Instance.ThereIsEmptySlot(ref targetSlot))
        {
            equipment.IsEquipped = false;
            equipment.OnUnEquip();
            UnwearEquipment(equipment.ItemIndex);
            OnItemUnEquipped?.Invoke(equipment, targetSlot);
        }

        else
        {
            Debug.Log("INVENTORY IS FULL CAN NOT UNEQUÝP");
        }
    }

    public void ChangeItem(IEquipment sourceEquipment, EquipmentSlot targetSlot, Item sourceItem)
    {
        ItemUIIcon targetIcon = targetSlot.GetComponentInChildren<ItemUIIcon>();
        IEquipment targetEquipment = targetIcon.item as IEquipment;
        sourceEquipment.IsEquipped = true;
        targetEquipment.IsEquipped = false;
        targetEquipment.OnUnEquip();
        UnwearEquipment(targetEquipment.ItemIndex);
        sourceEquipment.OnEquip();
        WearEquipment(sourceEquipment.ItemIndex);
        OnItemChanged?.Invoke(sourceEquipment, sourceItem);
    }

    public void WearEquipment(int itemIndex)
    {
        GameObject equipment = GetEquipableItem(itemIndex);
        equipment.SetActive(true);
    }
    public void UnwearEquipment(int itemIndex)
    {
        GameObject equipment = GetEquipableItem(itemIndex);
        equipment.SetActive(false);
    }
    public GameObject GetEquipableItem(int itemIndex)
    {
        return EquipableItems[itemIndex];
    }

    public void ChangeEquipmentState()
    {
        OnEquipmentWindowStateChange?.Invoke();
    }
}
