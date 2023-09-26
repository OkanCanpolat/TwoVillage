using UnityEngine;

public class DialoguePanelManager : MonoBehaviour
{
    public static DialoguePanelManager Instance;
    public GameObject OptionMenuObject;
    public GameObject OptionMenuContent;
    public GameObject DialogueOptionPrefab;
    public GameObject MonologMenuPrefab;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void CreateOptionObject(DialogueNode node, GameObject questGiver)
    {
        GameObject go = Instantiate(DialogueOptionPrefab, OptionMenuContent.transform);
        DialogueMenuOption option = go.GetComponent<DialogueMenuOption>();
        option.Node = node;
        option.QuestGiver = questGiver;
        option.InitOptionNode();
    }
    public void ClearOptionNodes()
    {
        foreach (Transform child in OptionMenuContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void ChangeOptionPanelState()
    {
        ClearOptionNodes();
        bool state = OptionMenuObject.activeSelf;
        OptionMenuObject.SetActive(!state);
    }
    public void OpenOptionPanel()
    {
        if (!OptionMenuObject.activeSelf)
        {
            ClearOptionNodes();
            OptionMenuObject.SetActive(true);
        }
    }
    public void CloseOptionPanel()
    {
        OptionMenuObject.SetActive(false);

    }
    public void CreateMonologPanel(MonologueNode node,GameObject questGiver)
    {
        GameObject go = Instantiate(MonologMenuPrefab, questGiver.transform.position, Quaternion.identity);
        MonologPanel panel = go.GetComponent<MonologPanel>();
        panel.Node = node;
    }
}
