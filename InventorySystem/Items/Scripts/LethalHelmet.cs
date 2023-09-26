
using UnityEngine;

[CreateAssetMenu(fileName = "Lethal Helmet", menuName = "Inventory System/Items/Lethal Helmet")]

public class LethalHelmet : Item, IEquipment
{
    public EquipmentType Type { get => type; set => type = value; }
    public bool IsEquipped { get; set; }
    public int ItemIndex => itemIndex;

    [SerializeField] private EquipmentType type;
    [SerializeField] private int itemIndex;
    [SerializeField] private int armourBonus;
    [SerializeField] private float healthBonus;

    private void Awake()
    {
        IsEquipped = false;
    }
    private void OnValidate()
    {
        IsEquipped = false;

    }
    public void OnEquip()
    {
        PlayerStatsContainer.Instance.PHealth.ChangeArmour(armourBonus);
        PlayerStatsContainer.Instance.PHealth.IncreaseMaxHealth(healthBonus);

    }

    public void OnUnEquip()
    {
        PlayerStatsContainer.Instance.PHealth.ChangeArmour(-armourBonus);
        PlayerStatsContainer.Instance.PHealth.DecreaseMaxHealth(healthBonus);

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
}
