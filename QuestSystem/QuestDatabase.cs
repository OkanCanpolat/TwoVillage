using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public static QuestDatabase Instance;
    public List<QuestDatabaseDictionary> Quests;

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
    }
    public Quest GetQuestByID(int id)
    {
        foreach (QuestDatabaseDictionary quest in Quests)
        {
            if (id == quest.ID)
            {
                return quest.Quest;
            }
        }
        return null;
    }
}
