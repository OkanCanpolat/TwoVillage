using UnityEngine;

[CreateAssetMenu(fileName = "QuestSteps", menuName = "Quest System/Quest Steps/Kill Orc", order = 5)]
public class KillOrc : QuestStep
{
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount = 0;

    private void Awake()
    {
        currentAmount = 0;
    }
    private void OnValidate()
    {
        currentAmount = 0;
    }

    public override void InitializeStep(Quest quest)
    {
        this.quest = quest;
        IsFinished = false;
        currentAmount = 0;
        QuestEvents.Instance.OnOrcDied += Evaluate;
    }

    public override void DestructStep()
    {
        QuestEvents.Instance.OnOrcDied -= Evaluate;
    }

    private void Evaluate()
    {
        currentAmount++;

        if (currentAmount >= requiredAmount && !IsFinished)
        {
            IsFinished = true;
            InvokeOnFinish();
            quest.ChangeState(QuestState.CanFinish);
            quest.NPC.ControlQuestIndicator();
        }
    }
}
