using UnityEngine;

public class ShopNPC : MonoBehaviour, IInteractable
{
    public ShopWindow ShopWindow;
    public float InteractRadius;
    public bool DrawGizmo;
    private bool inConnecttion;

    private void Update()
    {
        if (inConnecttion)
        {
            float distance = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
            if (distance > InteractRadius)
            {
                inConnecttion = false;
                if(ShopManager.Instance.isActive())
                {
                    ShopManager.Instance.DeactivateShopWindow();
                    ShopManager.Instance.CurrentWindow = null;
                }
            }
        }
    }

    public void Interact()
    {
        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance < InteractRadius)
        {
            ShopManager.Instance.CurrentWindow = ShopWindow;
            ShopManager.Instance.ActivateShopWindow();
            inConnecttion = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, InteractRadius);
        }
    }
}
