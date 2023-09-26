using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentUIManager : MonoBehaviour
{
    public List<EquipmentSlot> Slots;
    public GameObject EquipmentPanelObject;
    public TMP_Text HealthText;
    public TMP_Text ArmourText;
    public TMP_Text DamageText;
    public TMP_Text MovementSpeedText;

    private void Start()
    {
        EquipmentManager.Instance.OnItemEquipped += OnItemEquipped;
        EquipmentManager.Instance.OnItemUnEquipped += OnItemUnEquipped;
        EquipmentManager.Instance.OnItemChanged += OnItemChanged;
        EquipmentManager.Instance.OnEquipmentWindowStateChange += ChangePanelState;
        PlayerStatsContainer.Instance.PHealth.OnMaxHealChanged += OnHealthChanged;
        PlayerStatsContainer.Instance.PHealth.OnArmourChanged += OnArmourChanged;
        PlayerStatsContainer.Instance.PAttack.OnDamageChanged += OnAttackDamageChanged;
        PlayerStatsContainer.Instance.PMovement.OnMovementSpeedChanged += OnMovementSpeedChanged;

        HealthText.text = PlayerStatsContainer.Instance.PHealth.GetMaximumHealth().ToString();
        DamageText.text = PlayerStatsContainer.Instance.PAttack.AttackDamage.ToString();
        ArmourText.text = PlayerStatsContainer.Instance.PHealth.GetArmour().ToString();
        MovementSpeedText.text = PlayerStatsContainer.Instance.PMovement.MovementSpeed.ToString();
    }
    public void OnItemEquipped(IEquipment equipment, Item item)
    {
        ItemUIIcon icon = InventoryUIManager.Instance.GetIcon(item);
        EquipmentSlot target = EquipmentManager.Instance.GetSlot(equipment.Type);
        icon.transform.SetParent(target.transform);
        icon.Dragable = false;
    }

    public void OnItemUnEquipped(IEquipment targetEquipment, InventorySlot targetSlot)
    {
        EquipmentSlot slot = EquipmentManager.Instance.GetSlot(targetEquipment.Type);
        ItemUIIcon icon = slot.transform.GetComponentInChildren<ItemUIIcon>();
        InventoryUIManager.Instance.PlaceIconToSlot(icon, targetSlot);
        icon.Dragable = true;
    }
    public void OnItemChanged(IEquipment sourceEquipment, Item sourceItem)
    {
        ItemUIIcon sourceIcon = InventoryUIManager.Instance.GetIcon(sourceItem);
        InventorySlot sourceSlot = sourceIcon.GetComponentInParent<InventorySlot>();
        EquipmentSlot target = EquipmentManager.Instance.GetSlot(sourceEquipment.Type);
        ItemUIIcon targetIcon = target.transform.GetComponentInChildren<ItemUIIcon>();
        sourceIcon.transform.SetParent(target.transform);
        targetIcon.transform.SetParent(sourceSlot.transform);
        sourceIcon.Dragable = false;
        targetIcon.Dragable = true;
    }

    public void OnHealthChanged(float maxHealth)
    {
        HealthText.text = maxHealth.ToString();
    }
    public void OnArmourChanged(int armour)
    {
        ArmourText.text = armour.ToString();
    }
    public void OnAttackDamageChanged(int damage)
    {
        DamageText.text = damage.ToString();
    }
    public void OnMovementSpeedChanged(float speed)
    {
        MovementSpeedText.text = speed.ToString();
    }
    public void ChangePanelState()
    {
        bool state = EquipmentPanelObject.activeSelf;
        EquipmentPanelObject.SetActive(!state);
    }
}
