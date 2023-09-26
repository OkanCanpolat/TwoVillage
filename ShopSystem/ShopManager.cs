using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public ShopWindow CurrentWindow;

    public bool isActive()
    {
        return CurrentWindow.gameObject.activeSelf;
    }
    public void ActivateShopWindow()
    {
        CurrentWindow.gameObject.SetActive(true);
        PlayerInventory.Instance.currentState = PlayerInventory.Instance.ShopState;
    }
    public void DeactivateShopWindow()
    {
        CurrentWindow.gameObject.SetActive(false);
        PlayerInventory.Instance.currentState = PlayerInventory.Instance.NormalState;
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
