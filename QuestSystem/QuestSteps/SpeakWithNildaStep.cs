using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Speak Nilda Step" , menuName = "Quest System/Quest Steps/Speak Nilda")]
public class SpeakWithNildaStep : QuestStep
{
    public override void InitializeStep(Quest quest)
    {
        this.quest = quest;
        IsFinished = false;
    }
}
