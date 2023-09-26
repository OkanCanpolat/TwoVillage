using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerQuestManager questManager;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerInventory playerInventory;
    private EquipmentManager playerEquipment;
    private PlayerHealth playerHealth;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HotbarController hotbatController;
    [SerializeField] private MapUIManager map;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        questManager = GetComponent<PlayerQuestManager>();
        playerInventory = GetComponent<PlayerInventory>();
        playerEquipment = GetComponent<EquipmentManager>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        HandleInputs();
    }
    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ChangeMenuPanelState();
        }

        if (playerHealth.IsDead) return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            questManager.ControlPanel();
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            playerMovement.ControlRay();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAttack.HandleInput();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerInventory.ChangeInventoryState();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerEquipment.ChangeEquipmentState();
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            cameraController.ChangeCameraRotation();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hotbatController.UseHotbar(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hotbatController.UseHotbar(1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.ChangeMpaState();
        }
        
    }
}
