using UnityEngine;

[CreateAssetMenu(fileName = "Basic Sword", menuName = "Inventory System/Items/Basic Sword")]
public class BasicSword : Item, IEquipment
{
    public EquipmentType Type { get => type; set => type = value; }
    public bool IsEquipped { get; set; }
    public int ItemIndex => itemIndex;
    [SerializeField] private int itemIndex;
    [SerializeField] private EquipmentType type;
    [SerializeField] private int damage;

    private void Awake()
    {
        IsEquipped = false;
    }
    private void OnValidate()
    {
        IsEquipped = false;
    }
    public override void UseItem()
    {
        if (!IsEquipped)
        {
            EquipmentManager.Instance.Equip(this, this);
            IsEquipped = true;
        }

        else
        {
            EquipmentManager.Instance.UnEquip(this);
        }
    }

    public void OnEquip()
    {
        GameObject weapon = EquipmentManager.Instance.GetEquipableItem(itemIndex);
        Collider weaponCollider = weapon.GetComponent<Collider>();
        PlayerStatsContainer.Instance.PAttack.SetWeaponCollider(weaponCollider);
        PlayerStatsContainer.Instance.PAttack.isWeaponEquipped = true;
        PlayerStatsContainer.Instance.PAttack.ChangeDamage(damage);
    }

    public void OnUnEquip()
    {
        PlayerStatsContainer.Instance.PAttack.ChangeDamage(-damage);
        PlayerStatsContainer.Instance.PAttack.isWeaponEquipped = false;
    }
}
