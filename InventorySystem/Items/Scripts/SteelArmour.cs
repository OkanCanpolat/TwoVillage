using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Steel Armour", menuName = "Inventory System/Items/Steel Armour")]

public class SteelArmour : Item, IEquipment
{
    public EquipmentType Type { get => type; set => type = value; }
    public bool IsEquipped { get; set; }
    public int ItemIndex => itemIndex;
    public int NakedPartIndex => NakedPartIndex;

    [SerializeField] private int itemIndex;
    [SerializeField] private int nakedPartIndex;
    [SerializeField] private EquipmentType type;

    [SerializeField] private float healthBonus;
    [SerializeField] private int armourBonus;
    [SerializeField] private float movementSpeedDebuff;
    private void Awake()
    {
        IsEquipped = false;
    }
    private void OnValidate()
    {
        IsEquipped = false;

    }
    private void OnEnable()
    {
        IsEquipped = false;
    }
    public void OnEquip()
    {
        PlayerStatsContainer.Instance.PHealth.ChangeArmour(armourBonus);
        PlayerStatsContainer.Instance.PHealth.IncreaseMaxHealth(healthBonus);
        PlayerStatsContainer.Instance.PMovement.ChangeSpeed(movementSpeedDebuff);
    }
    public void OnUnEquip()
    {
        PlayerStatsContainer.Instance.PHealth.ChangeArmour(-armourBonus);
        PlayerStatsContainer.Instance.PHealth.IncreaseMaxHealth(-healthBonus);
        PlayerStatsContainer.Instance.PMovement.ChangeSpeed(-movementSpeedDebuff);
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
