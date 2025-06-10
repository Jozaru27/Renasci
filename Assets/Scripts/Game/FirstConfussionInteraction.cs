using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstConfussionInteraction : MonoBehaviour
{
    [SerializeField] string[] textNames;
    [TextArea(4, 6)][SerializeField] string[] sentences;

    public void OpenConfussionPanel()
    {
        if (!GameManager.Instance.firstConfussion)
        {
            foreach (string currentSentence in sentences)
            {
                InfoPanel.Instance.AddText(textNames[0], currentSentence, 1f);
            }

            GameManager.Instance.firstConfussion = true;
        }
    }

    public void ChangeName(string newName)
    {
        textNames[0] = newName;
    }

    public void ChangeDesc(string newDesc)
    {
        sentences[0] = newDesc;
    }
}
