using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuffIndicatorUIManager : MonoBehaviour
{
    public static BuffIndicatorUIManager Instance;
    public GameObject BuffIconPrefab;
    public GameObject DescriptionPanel;
    public TMP_Text DescriptionText;
    public GameObject Content;

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
    public void CreateBuffIcon(int time, string description, Sprite icon)
    {
        GameObject go = Instantiate(BuffIconPrefab, Content.transform);
        BuffIndicatorIcon buffIcon = go.GetComponent<BuffIndicatorIcon>();
        buffIcon.Init(time, description, icon);
    }

    public void OpenDescriptionPanel(string description)
    {
        DescriptionPanel.SetActive(true);
        DescriptionText.text = description;
    }
    public void CloseDescriptionPanel()
    {
        DescriptionPanel.SetActive(false);
    }

}
