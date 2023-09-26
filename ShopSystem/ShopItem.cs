using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public string ItemName;
    public Sprite ItemIcon;
    public int Price;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private int extraSellPricePercantage = 50;

    public void Init(Item item)
    {
        this.item = item;
        ItemName = item.ItemName;
        ItemIcon = item.Sprite;
        Price = CalculatePrice(item.Price);
    }
    public void LoadUI()
    {
        itemNameText.text = ItemName;
        priceText.text = Price.ToString();
        itemIconImage.sprite = ItemIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            ShopManager.Instance.CurrentWindow.Remove(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryUIManager.Instance.OpenDescriptionPanel(item.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUIManager.Instance.CloseDescriptionPanel();
    }

    private int CalculatePrice(int price)
    {
        float percantage = extraSellPricePercantage / 100f;
        float extraPrice = price * percantage;

        price += (int) extraPrice;
        return price;
    }
}
