using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstConfussionInteraction : MonoBehaviour
{
    [SerializeField] string[] sentences;

    public void OpenConfussionPanel()
    {
        if (!GameManager.Instance.firstConfussion)
        {
            foreach (string currentSentence in sentences)
            {
                InfoPanel.Instance.AddText(string.Empty, currentSentence, 1f);
            }

            GameManager.Instance.firstConfussion = true;
        }
    }
}
