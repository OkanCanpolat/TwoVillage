using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopWindow : MonoBehaviour
{
    public GameObject ShopItemPrefab;
    public GameObject Content;
    public ItemType type;
    public abstract bool Add(Item item);

    public abstract void Remove(ShopItem shopItem);
    
    
}
