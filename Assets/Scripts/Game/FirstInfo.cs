using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstInfo : MonoBehaviour
{
    [TextArea(4, 6)][SerializeField] string[] info;

    [SerializeField] Sprite[] infoImages;

    private void Start()
    {
        StartCoroutine(InitiatingTexts());
    }

    IEnumerator InitiatingTexts()
    {
        yield return null;

        foreach (string currentInfo in info)
        {
            InfoPanel.Instance.AddText(string.Empty, currentInfo, 1f);
        }
    }
}
