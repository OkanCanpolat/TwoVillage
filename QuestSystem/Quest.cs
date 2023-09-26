using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest : MonoBehaviour
{
    public PlayerQuestManager PlayerQuestManager;
    public List<QuestStep> Steps;
    public List<Quest> PreRequirements;
    public QuestNPC NPC { get => npc; }
    public QuestState state;
    [Header("Rewards")]
    public int GoldReward;
    [Header("Quest Events")]
    public UnityEvent OnQuestFinish;
    public UnityEvent OnQuestStart;
    [Header("Node Events")]
    public DialogueNode StartDialogueNode;
    public DialogueNode CanFinishDialogueNode;

    public QuestStep currentStep;
    public string QuestTitle;
    private QuestNPC npc;

    private void Awake()
    {
        npc = GetComponent<QuestNPC>();
    }
    public void ActivateQuestIfValid()
    {
        if (IsPreRequirementsFinished())
        {
            state = QuestState.Active;
            npc.ControlQuestIndicator();
        }
    }
    public bool IsPreRequirementsFinished()
    {
        bool result = true;

        if (PreRequirements != null)
        {
            foreach (Quest quest in PreRequirements)
            {
                bool current = quest.state == QuestState.Finished;
                result = result && current;
            }
        }
        return result;
    }
    private void SetUpCurrentStep()
    {
        if (IsStepExist())
        {
            SetCurrentStep();
            currentStep.InitializeStep(this);
            currentStep.OnStepFinish += SetNextStep;
        }
        else
        {
            state = QuestState.Finished;
            OnQuestFinish?.Invoke();
        }
    }
    public void SetNextStep()
    {
        RemoveCurrentStep();
        SetUpCurrentStep();
    }

    public void RemoveCurrentStep()
    {
        currentStep.DestructStep();
        Steps.Remove(currentStep);
    }
    private void SetCurrentStep()
    {
        currentStep = Steps[0];
    }
    public bool IsStepExist()
    {
        return Steps.Count > 0;
    }
    public void ChangeState(QuestState newState)
    {
        state = newState;
    }
    public void StartQuest()
    {
        OnQuestStart?.Invoke();
        SetUpCurrentStep();
        state = QuestState.InProgress;
        npc.ControlQuestIndicator();
    }

    public void FinishQuest()
    {
        state = QuestState.Finished;
        OnQuestFinish?.Invoke();
    }

    public void RewardPlayer()
    {
        PlayerInventory.Instance.AddGold(GoldReward);
    }
}
