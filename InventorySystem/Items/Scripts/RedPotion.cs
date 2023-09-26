using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Red Potion", menuName = "Inventory System/Items/Red Potion")]
public class RedPotion : Item
{
    public float HealAmount;
    public override void UseItem()
    {
        PlayerStatsContainer.Instance.PHealth.HealInstantly(HealAmount);
        PlayerInventory.Instance.RemoveItem(this);
    }
}
