using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Health Generation Potion", menuName = "Inventory System/Items/Health Generation Potion")]

public class HealthGenerationPotion : Item
{
    [TextArea(5, 10)]
    [SerializeField] private string buffIconDescription;
    [Tooltip ("Per 5 Second")]
    [SerializeField] private float healthGenerationBonus;
    [SerializeField] private int buffDuration;
    [SerializeField] private bool useable = true;

    private void Awake()
    {
        useable = true;
    }
    public override void UseItem()
    {
        if (!useable) return;

        PlayerStatsContainer.Instance.PHealth.ChangeHealthGenerationRateTeporary(buffDuration, healthGenerationBonus);
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
