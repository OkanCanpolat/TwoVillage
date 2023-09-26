using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueMenuOption : MonoBehaviour
{
    public DialogueNode Node;
    public Button button;
    public TMP_Text Text;
    public GameObject QuestGiver;

    public void ControlNextNodeAndCreate()
    {
        DialoguePanelManager.Instance.ClearOptionNodes();

        if (Node.AnswerNodes.Count > 0)
        {
            foreach (DialogueNode node in Node.AnswerNodes)
            {
                DialoguePanelManager.Instance.CreateOptionObject(node, QuestGiver);
            }
        }
        else if (Node.MonologNode.Texts.Count > 0)
        {
            DialoguePanelManager.Instance.CloseOptionPanel();
            DialoguePanelManager.Instance.CreateMonologPanel(Node.MonologNode, QuestGiver);
        }

        else
        {
            DialoguePanelManager.Instance.CloseOptionPanel();
        }
    }

    public void InitOptionNode()
    {
        List<UnityAction> list = UnityEventConverter.ConvertEventToAction(Node.OnClick);

        if (list != null)
        {
            foreach(UnityAction act in list)
            {
                button.onClick.AddListener(act);
            }
        }
        Text.text = Node.DialogueText.Text;
    }
}
