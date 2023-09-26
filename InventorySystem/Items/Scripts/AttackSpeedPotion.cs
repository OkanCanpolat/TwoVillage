using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Attack Speed Potion", menuName = "Inventory System/Items/Attack Speed Potion")]
public class AttackSpeedPotion : Item
{
    [TextArea(5, 10)]
    [SerializeField] private string buffIconDescription;
    [SerializeField] private float attackSpeedBonus;
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
        PlayerStatsContainer.Instance.PAttack.ChangeAttackSpeedTemporary(buffDuration, attackSpeedBonus);
        PlayerInventory.Instance.RemoveItem(this);
        BuffIndicatorUIManager.Instance.CreateBuffIcon(buffDuration, buffIconDescription, Sprite);
        useable = false;
        PreventBuffStack();
    }
    private async Task PreventBuffStack()
    {
        await Task.Delay(buffDuration * 1000);
        useable = true;
    }
}
