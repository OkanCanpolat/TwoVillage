using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologPanel : MonoBehaviour
{
    public MonologueNode Node;
    public TMP_Text Text;
    public int TimeBetweenNodes;

    private void Start()
    {
        transform.forward = Camera.main.transform.forward;
        StartCoroutine(StartMonolog());
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public IEnumerator StartMonolog()
    {
        foreach (DialogueText text in Node.Texts)
        {
            Text.text = text.Text;
            yield return new WaitForSeconds(TimeBetweenNodes);
        }
        Destroy(gameObject);
    }
}
