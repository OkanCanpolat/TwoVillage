using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSteps", menuName = "Quest System/Quest Steps/CollectWood", order = 5)]
public class CollectWoodQuestStep : QuestStep
{
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount = 0;
    [SerializeField] private GameObject woodPrefab;
    [Range (2, 7)]
    [SerializeField] private float maxXSpawnDistance;
    [Range(2, 7)]
    [SerializeField] private float maxZSpawnDistance;


    private void Awake()
    {
        currentAmount = 0;
        DynamicDescription = currentAmount + " / 5";
    }
    private void OnValidate()
    {
        currentAmount = 0;
        DynamicDescription = currentAmount + " / 5";
    }
    public override void InitializeStep(Quest quest)
    {
        this.quest = quest;
        IsFinished = false;
        currentAmount = 0;
        QuestEvents.Instance.OnWoodCollected += Evaluate;

        for (int i = 0; i < requiredAmount; i++)
        {
            float randomX = Random.Range(2, maxXSpawnDistance);
            float randomZ = Random.Range(2, maxZSpawnDistance);
            GameObject go = Instantiate(woodPrefab);
            Vector3 pos = go.transform.position;
            pos = new Vector3(pos.x + randomX, pos.y, pos.z + randomZ);
            go.transform.position = pos;
        }
    }
    public override void DestructStep()
    {
        QuestEvents.Instance.OnWoodCollected -= Evaluate;
    }

    private void Evaluate()
    {
        currentAmount++;
        UploadDynamicDescription();
        if (currentAmount >= requiredAmount && !IsFinished)
        {
            IsFinished = true;
            OnFinish();
            InvokeOnFinish();
        }
    }

    public override void UploadDynamicDescription()
    {
        DynamicDescription = currentAmount + " / 5";
    }

    private void OnFinish()
    {
        Quest quest = QuestDatabase.Instance.GetQuestByID(0);
        quest.ActivateQuestIfValid();
    }
}
