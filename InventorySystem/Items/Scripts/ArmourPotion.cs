using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Armour Potion", menuName = "Inventory System/Items/Armour Potion")]

public class ArmourPotion : Item
{
    [TextArea (5, 10)]
    [SerializeField] private string buffIconDescription;
    [SerializeField] private int armourBonus;
    [SerializeField] private int buffDuration;
    [SerializeField] private bool useable = true;

    private void Awake()
    {
        useable = true;
    }
    private void OnValidate()
    {
        useable = true;

    }
    public override void UseItem()
    {
        if (!useable) return;
        PlayerStatsContainer.Instance.PHealth.ChangeArmourTemporary(buffDuration, armourBonus);
        PlayerInventory.Instance.RemoveItem(this);
        useable = false;
        BuffIndicatorUIManager.Instance.CreateBuffIcon(buffDuration, buffIconDescription, Sprite);
        PreventBuffStack();
    }

    private async Task PreventBuffStack()
    {
        await Task.Delay(buffDuration * 1000);
        useable = true;
    }
}
