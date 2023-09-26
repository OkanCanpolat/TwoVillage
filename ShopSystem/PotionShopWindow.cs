using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShopWindow : ShopWindow
{
    public override bool Add(Item item)
    {
        if (type != item.ItemType) return false;

        PlayerInventory.Instance.AddGold(item.Price);
        return true;
    }

    public override void Remove(ShopItem shopItem)
    {
        if ((PlayerInventory.Instance.HasItem(shopItem.item) || PlayerInventory.Instance.ThereIsEmptySlot()) &&
             PlayerInventory.Instance.GoldAmount >= shopItem.Price)
        {
            PlayerInventory.Instance.RemoveGold(shopItem.Price);
            PlayerInventory.Instance.AddItem(shopItem.item);
        }
    }
}
