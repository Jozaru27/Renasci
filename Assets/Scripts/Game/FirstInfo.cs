using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstInfo : MonoBehaviour
{
    [TextArea(4, 6)][SerializeField] string[] info;

    [SerializeField] Sprite[] infoImages;

    private void Start()
    {
        foreach (string currentInfo in info)
        {
            InfoPanel.Instance.AddText(string.Empty,currentInfo, 0.125f);
        }
    }
}
