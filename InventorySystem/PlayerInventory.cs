using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public List<Item> Items;
    public List<Item> StartingItems;
    public List<InventorySlot> Slots;
    public Action<Item, InventorySlot> OnItemAdded;
    public Action<Item> OnItemRemoved;
    public Action OnInventoryOpenClose;
    public Action<Item> OnItemAmountUpdate;
    public Action<int> OnGoldChanged;
    public int GoldAmount;

    public IInventoryState currentState;
    public InventoryNormalState NormalState;
    public InventoryShopState ShopState;
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

        NormalState = new InventoryNormalState();
        ShopState = new InventoryShopState();
        currentState = NormalState;
    }
    private void Start()
    {
        foreach(Item item in StartingItems)
        {
            AddItem(item);
            IEquipment equipment = item as IEquipment;
            equipment.IsEquipped = false;
            item.UseItem();
        }
    }
    public void AddItem(Item item)
    {
        InventorySlot targetSlot = null;

        if (ThereIsEmptySlot(ref targetSlot))
        {
            if (item.Stackable)
            {
                if (HasItem(item))
                {
                    item.Amount++;
                    OnItemAmountUpdate?.Invoke(item);
                }

                else
                {
                    item.Amount++;
                    Items.Add(item);
                    OnItemAdded(item, targetSlot);
                }
            }

            else
            {
                Items.Add(item);
                OnItemAdded(item, targetSlot);
            }
            
        }

        else
        {
            Debug.Log("Inventory Full");
        }
       
    }
    public void RemoveItem(Item item)
    {
        if (item.Stackable)
        {
            if(item.Amount <= 1)
            {
                item.Amount--;
                Items.Remove(item);
                OnItemRemoved?.Invoke(item);
            }

            else
            {
                item.Amount--;
                OnItemAmountUpdate?.Invoke(item);
            }
        }

        else
        {
            Items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }
       
    }
    public bool ThereIsEmptySlot(ref InventorySlot target)
    {
        foreach(InventorySlot slot in Slots)
        {
            if (slot.IsEmpty())
            {
                target = slot;
                return true;
            }
        }
        return false;
    }
    public bool ThereIsEmptySlot()
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot.IsEmpty())
            {
                return true;
            }
        }
        return false;
    }
    public void ChangeInventoryState()
    {
        OnInventoryOpenClose?.Invoke();
        
    }
    public bool HasItem(Item item)
    {
        foreach(Item itm in Items)
        {
            if(item == itm)
            {
                return true;
            }
        }
        return false;
    }
    public void AddGold(int value)
    {
        GoldAmount += value;
        OnGoldChanged?.Invoke(GoldAmount);
    }
    public void RemoveGold(int value)
    {
        GoldAmount -= value;
        OnGoldChanged?.Invoke(GoldAmount);
    }

}
