
using UnityEngine;

[CreateAssetMenu(fileName = "Lethal Leg Armour", menuName = "Inventory System/Items/Lethal Leg Armour")]

public class LethalLegArmour : Item, IEquipment
{
    public EquipmentType Type { get => type; set => type = value; }
    public bool IsEquipped { get; set; }
    public int ItemIndex => itemIndex;
    public int NakedPartIndex => NakedPartIndex;


    [SerializeField] private EquipmentType type;
    [SerializeField] private int itemIndex;
    [SerializeField] private int nakedPartIndex;

    [SerializeField] private float healthBonus;
    [SerializeField] private int armourBonus;

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
        EquipmentManager.Instance.UnwearEquipment(nakedPartIndex);

    }

    public void OnUnEquip()
    {
        PlayerStatsContainer.Instance.PHealth.ChangeArmour(-armourBonus);
        PlayerStatsContainer.Instance.PHealth.DecreaseMaxHealth(healthBonus);
        EquipmentManager.Instance.WearEquipment(nakedPartIndex);

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
