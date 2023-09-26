using UnityEngine;

public class Item : ScriptableObject
{
    public ItemType ItemType;
    public int Price;
    public string ItemName;
    public Sprite Sprite;
    [TextArea (10, 50)]
    public string Description;
    public bool Stackable;
    public int Amount;
    public virtual void UseItem() { }
}
