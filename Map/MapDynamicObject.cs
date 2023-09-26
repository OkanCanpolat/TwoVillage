using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDynamicObject : MonoBehaviour
{
    [SerializeField] private Transform correspondingRealWordObject;
    [SerializeField] private MapUIManager mapManager;
    private RectTransform imagePosition;

    private void Awake()
    {
        imagePosition = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        mapManager.WorldToMapPoint(correspondingRealWordObject, imagePosition);
        imagePosition.rotation = Quaternion.Euler(0, 0, -correspondingRealWordObject.eulerAngles.y);
    }
}
