using System.Collections.Generic;
using UnityEngine;

public class JournalPanel : MonoBehaviour
{
    public GameObject QuestPanelContent;
    public GameObject QuestPanel;
    public GameObject QuestDescriptionObject;
    public GameObject QuestPanelNodeObject;
    public void CheckPanelStateAndManage(List<Quest> quests)
    {
        bool active = QuestPanel.activeSelf;
        if (active)
        {
            QuestPanel.SetActive(false);
            QuestDescriptionObject.SetActive(false);
        }
        
        else
        {
            QuestPanel.SetActive(true);
            CreateNodes(quests);
        }
    }
    public void CreateNodes(List<Quest> quests)
    {
        CleanPanel();
        foreach(Quest quest in quests)
        {
            GameObject go = Instantiate(QuestPanelNodeObject, QuestPanelContent.transform);
            PlayerQuestPanelNode node = go.GetComponent<PlayerQuestPanelNode>();
            node.InitNode(QuestDescriptionObject, quest);
        }
    }

    public void CleanPanel()
    {
        foreach(Transform child in QuestPanelContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
