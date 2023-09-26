using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Dialogue Text" , menuName = "Dialogue System/ Dialogue Text")]
public class DialogueText : ScriptableObject
{
    [TextArea(6, 12)]
    public string Text;
}
