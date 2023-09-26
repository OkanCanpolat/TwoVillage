using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lethal Armour", menuName = "Inventory System/Items/Lethal Armour")]

public class LethalArmour : Item, IEquipment
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
}
