using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    public static MapUIManager Instance;
    private float screenX;
    private float screenY;
    [SerializeField] private float worldWidth;
    [SerializeField] private float worldHeight;
    [SerializeField] private GameObject mapObject;
    [SerializeField] private Image questPlaceIndicator;
    [SerializeField] private Transform placeIndicatorParent;

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
        screenX = Screen.width;
        screenY = Screen.height;

    }
    public void WorldToMapPoint(Transform worldPosition, RectTransform image)
    {
        Vector3 worldPos = worldPosition.position;
        float worldXPercantage = worldPos.x / worldWidth;
        float worldZPercantage = worldPos.z / worldHeight;

        float mapX = screenX * worldXPercantage;
        float mapY = screenY * worldZPercantage;

        image.anchoredPosition = new Vector2(mapX, mapY);
    }
    public void WorldToMapPoint(Vector3 worldPosition, RectTransform image)
    {
        float worldXPercantage = worldPosition.x / worldWidth;
        float worldZPercantage = worldPosition.z / worldHeight;

        float mapX = screenX * worldXPercantage;
        float mapY = screenY * worldZPercantage;

        image.anchoredPosition = new Vector2(mapX, mapY);
    }
    public void ActivatePlaceIndicator(Vector3 worldPosition)
    {
        questPlaceIndicator.gameObject.SetActive(true);
        WorldToMapPoint(worldPosition, questPlaceIndicator.rectTransform);
    }
    public void DeactivatePlaceIndicator()
    {
        questPlaceIndicator.gameObject.SetActive(false);
    }
    public void ChangeMpaState()
    {
        bool state = mapObject.activeSelf;
        mapObject.SetActive(!state);
    }
}
