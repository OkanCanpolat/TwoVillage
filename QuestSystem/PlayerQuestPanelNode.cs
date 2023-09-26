using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerQuestPanelNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject QuestDescriptionPanelObject;
    [SerializeField] private Sprite OnMouseSprite;
    [SerializeField] private TMP_Text QuestTitleText;
    private string description;
    private Image image;
    private JournalDescriptionPanel journalDescriptionPanel;
    private Quest correspondingQuest;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitNode(GameObject descrioptionPanel, Quest quest)
    {
        correspondingQuest = quest;
        QuestTitleText.text = correspondingQuest.QuestTitle;
        description = correspondingQuest.currentStep.Description;
        QuestDescriptionPanelObject = descrioptionPanel;
        journalDescriptionPanel = QuestDescriptionPanelObject.GetComponent<JournalDescriptionPanel>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = OnMouseSprite;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        QuestDescriptionPanelObject.SetActive(true);
        description = correspondingQuest.currentStep.Description;
        journalDescriptionPanel.Initialize(QuestTitleText.text, description, correspondingQuest.currentStep.DynamicDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = null;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        QuestDescriptionPanelObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Vector3 pos = new Vector3(correspondingQuest.currentStep.WorldX, 0, correspondingQuest.currentStep.WorldZ);
            MapUIManager.Instance.ActivatePlaceIndicator(pos);
        }
    }
}
