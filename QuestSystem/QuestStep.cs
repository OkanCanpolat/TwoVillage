using System;
using UnityEngine;


public class QuestStep : ScriptableObject
{
    public bool IsFinished;
    public Action OnStepFinish;
    public string Title;
    [TextArea (5,10)]
    public string Description;
    [TextArea(5, 10)]
    public string DynamicDescription;
    public bool HasDynamicDescription;
    public float WorldX;
    public float WorldZ;
    protected Quest quest;

    public void InvokeOnFinish()
    {
        OnStepFinish?.Invoke();
    }

    public virtual void InitializeStep(Quest quest) { }
    
    public virtual void DestructStep() { }
    public virtual void UploadDynamicDescription() { }
}
