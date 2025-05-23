using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstInfo : MonoBehaviour
{
    [TextArea(4, 6)][SerializeField] string[] info;

    [SerializeField] Sprite[] infoImages;

    private void Start()
    {
        InfoPanel.Instance.ImageTextInfo(info[0], infoImages[0], 0.125f);
    }
}
