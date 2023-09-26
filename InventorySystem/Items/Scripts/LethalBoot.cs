
using UnityEngine;

[CreateAssetMenu(fileName = "Lethal Boot", menuName = "Inventory System/Items/Lethal Boot")]

public class LethalBoot : Item, IEquipment
{
    public EquipmentType Type { get => type; set => type = value; }
    public bool IsEquipped { get; set; }
    public int ItemIndex => itemIndex;
    public int NakedPartIndex => NakedPartIndex;


    [SerializeField] private EquipmentType type;
    [SerializeField] private int itemIndex;
    [SerializeField] private int nakedPartIndex;

    [SerializeField] private float movementSpeedBonus;

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
        PlayerStatsContainer.Instance.PMovement.ChangeSpeed(movementSpeedBonus);
        EquipmentManager.Instance.UnwearEquipment(nakedPartIndex);

    }

    public void OnUnEquip()
    {
        PlayerStatsContainer.Instance.PMovement.ChangeSpeed(-movementSpeedBonus);
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
