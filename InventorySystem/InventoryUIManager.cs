using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;
    public TMP_Text GoldText;
    public PlayerInventory Inventory;
    public List<InventorySlot> Slots;
    public GameObject InventoryUIIcon;
    public GameObject DescriptionPanel;
    public TMP_Text DescriptionText;
    public GameObject PlayerInventoryObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        GoldText.text = PlayerInventory.Instance.GoldAmount.ToString();

        Inventory.OnGoldChanged += OnGoldChanged;
        Inventory.OnInventoryOpenClose += ChangeInventoryState;
        Inventory.OnItemAdded += OnItemAdded;
        Inventory.OnItemAmountUpdate += UpdateItemAmountText;
        Inventory.OnItemRemoved += OnItemRemoved;
    }

    public void ChangeInventoryState()
    {
        bool current = PlayerInventoryObject.activeSelf;
        PlayerInventoryObject.SetActive(!current);
    }
    public void OnItemAdded(Item item, InventorySlot slot)
    {
        GameObject go = Instantiate(InventoryUIIcon, slot.transform);
        ItemUIIcon icon = go.GetComponent<ItemUIIcon>();
        icon.Initialize(item);
    }

    public void OnItemRemoved(Item item)
    {
        ItemUIIcon icon = GetIcon(item);
        Destroy(icon.gameObject);
        CloseDescriptionPanel();
    }

    public void OpenDescriptionPanel(string text)
    {
        DescriptionText.text = text;
        DescriptionPanel.SetActive(true);
    }
    public void CloseDescriptionPanel()
    {
        DescriptionPanel.SetActive(false);
    }
    public void UpdateItemAmountText(Item item)
    {
        ItemUIIcon icon = GetIcon(item);
        icon.AmountText.text = item.Amount.ToString();
    }
    public ItemUIIcon GetIcon(Item item)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (!slot.IsEmpty())
            {
                ItemUIIcon icon = slot.GetComponentInChildren<ItemUIIcon>();
                if (icon.item == item)
                {
                    return icon;
                }
            }
        }

        foreach (HotbarSlot slot in HotbarController.Instance.HotbarSlots)
        {
            if (!slot.IsEmpty())
            {
                ItemUIIcon icon = slot.GetComponentInChildren<ItemUIIcon>();
                if (icon.item == item)
                {
                    return icon;
                }
            }
        }

        return null;
    }
    public void PlaceIconToSlot(ItemUIIcon icon, InventorySlot targetSlot)
    {
        icon.transform.SetParent(targetSlot.transform);
    }
    public void OnGoldChanged(int value)
    {
        GoldText.text = value.ToString();
    }

}
