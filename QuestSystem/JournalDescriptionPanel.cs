using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalDescriptionPanel : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text Description;
    public TMP_Text DynamicDescription;
    public void Initialize(string title, string description, string dynamicDescription)
    {
        Title.text = title;
        Description.text = description;
        DynamicDescription.text = dynamicDescription;
    }
}
