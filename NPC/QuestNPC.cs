using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class QuestNPC : MonoBehaviour, IInteractable
{
    public float InteractRadius;
    public bool DrawGizmo;
    public List<Quest> Quests;
    public GameObject Headup;
    public GameObject QuestIndicator;
    private bool inConnecttion;

    private void Start()
    {
        ControlQuestIndicator();
    }
    private void Update()
    {
        if (inConnecttion)
        {
            float distance = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
            if (distance > InteractRadius)
            {
                inConnecttion = false;
                if (DialoguePanelManager.Instance.OptionMenuObject.gameObject.activeSelf)
                {
                    DialoguePanelManager.Instance.CloseOptionPanel();
                }
            }
        }
    }

    public void Interact()
    {
        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance < InteractRadius)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.state == QuestState.Active)
                {
                    DialoguePanelManager.Instance.OpenOptionPanel();
                    DialoguePanelManager.Instance.CreateOptionObject(quest.StartDialogueNode, Headup);
                }

                else if (quest.state == QuestState.CanFinish)
                {
                    DialoguePanelManager.Instance.OpenOptionPanel();
                    DialoguePanelManager.Instance.CreateOptionObject(quest.CanFinishDialogueNode, Headup);
                }
            }
            inConnecttion = true;
        }
    }

    public void ControlQuestIndicator()
    {
        foreach (Quest quest in Quests)
        {
            if (quest.state == QuestState.Active || quest.state == QuestState.CanFinish)
            {
                QuestIndicator.SetActive(true);
                return;
            }
        }

        QuestIndicator.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        if (DrawGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, InteractRadius);
        }
    }

}
