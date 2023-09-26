using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject respawnPanel;
    [SerializeField] private GameObject menuPanel;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.OnPlayerDie += OpenRespawnPanel;
    }
    public void Respawn()
    {
        player.position = spawnPoint.position;
        playerHealth.RespawnCharacter();
    }
    private void OpenRespawnPanel()
    {
        respawnPanel.SetActive(true);
    }
    public void ChangeMenuPanelState()
    {
        bool state = menuPanel.activeSelf;
        menuPanel.SetActive(!state);
    }
}
