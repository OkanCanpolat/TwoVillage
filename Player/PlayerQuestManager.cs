using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    public JournalPanel QuestPanel;
    public List<Quest> activeQuests;

    private void Awake()
    {
        activeQuests = new List<Quest>();
    }

    public void RemoveQuest(Quest quest)
    {
        activeQuests.Remove(quest);
    }
    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }

    public void ControlPanel()
    {
        QuestPanel.CheckPanelStateAndManage(activeQuests);
    }
}
