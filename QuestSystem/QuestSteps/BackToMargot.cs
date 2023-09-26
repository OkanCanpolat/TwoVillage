using UnityEngine;

[CreateAssetMenu(fileName = "QuestSteps", menuName = "Quest System/Quest Steps/Back To Margot", order = 5)]

public class BackToMargot : QuestStep
{
    public override void InitializeStep(Quest quest)
    {
        this.quest = quest;
        IsFinished = false;
    }
}
