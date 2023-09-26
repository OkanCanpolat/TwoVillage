using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Potion", menuName = "Inventory System/Items/Movement Potion")]

public class MovementSpeedPotion : Item
{
    [SerializeField] private float movementSpeedBonus;
    [SerializeField] private int buffDuration;
    [TextArea(5, 10)]
    [SerializeField] private string buffIconDescription;
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
        
        PlayerStatsContainer.Instance.PMovement.ChangeSpeedTemporary(buffDuration, movementSpeedBonus);
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
  
   