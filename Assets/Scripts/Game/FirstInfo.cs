using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstInfo : MonoBehaviour
{
    [TextArea(4, 6)][SerializeField] string[] info;

    [SerializeField] Sprite[] infoImages;

    private void Start()
    {
        InfoPanel.Instance.AddText(info[0], 0.125f);
    }
}
