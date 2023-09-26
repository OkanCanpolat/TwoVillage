using UnityEngine;

[CreateAssetMenu(fileName = "QuestSteps", menuName = "Quest System/Quest Steps/Kill Skeleton", order = 5)]

public class KillSkeleton : QuestStep
{
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount = 0;
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
        QuestEvents.Instance.OnSkeletonDied += Evaluate;
    }
    public override void DestructStep()
    {
        QuestEvents.Instance.OnSkeletonDied -= Evaluate;
    }

    private void Evaluate()
    {
        currentAmount++;
        UploadDynamicDescription();

        if (currentAmount >= requiredAmount && !IsFinished)
        {
            IsFinished = true;
            InvokeOnFinish();
            quest.ChangeState(QuestState.CanFinish);
            quest.NPC.ControlQuestIndicator();
        }
    }
    public override void UploadDynamicDescription()
    {
        DynamicDescription = currentAmount + " / 5";
    }
}
