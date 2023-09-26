using UnityEngine;

public class EquipmentShopWindow : ShopWindow
{
    public override bool Add(Item item)
    {
        if (type != item.ItemType) return false;

        PlayerInventory.Instance.AddGold(item.Price);
        GameObject newItem =Instantiate(ShopItemPrefab, Content.transform);
        ShopItem shopItem = newItem.GetComponent<ShopItem>();
        shopItem.Init(item);
        shopItem.LoadUI();
        return true;
    }

    public override void Remove(ShopItem shopItem)
    {
        if (PlayerInventory.Instance.ThereIsEmptySlot() &&
            PlayerInventory.Instance.GoldAmount >= shopItem.Price)
        {
            PlayerInventory.Instance.RemoveGold(shopItem.Price);
            PlayerInventory.Instance.AddItem(shopItem.item);
            Destroy(shopItem.gameObject);
        }
    }
}
