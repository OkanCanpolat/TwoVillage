using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class DialogueNode 
{
    public DialogueText DialogueText;
    public List<DialogueNode> AnswerNodes;
    public MonologueNode MonologNode;
    public UnityEvent OnClick;
}

