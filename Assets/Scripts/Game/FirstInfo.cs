using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FirstInfo : MonoBehaviour
{
    [TextArea(4, 6)][SerializeField] string[] info;

    [SerializeField] Sprite[] infoImages;
    [SerializeField] Image fadeImg;

    private void Start()
    {
        StartCoroutine(InitialFade());
    }

    IEnumerator InitialFade()
    {
        Time.timeScale = 0f;

        float timeElapsed = 0f;

        Color imageColor = Color.black;
        imageColor.a = 1f;
        fadeImg.color = imageColor;

        while (fadeImg.color.a > 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            imageColor.a -= 1 * 0.35f * Time.unscaledDeltaTime;
            fadeImg.color = imageColor;

            yield return null;
        }

        StartCoroutine(InitiatingTexts());
    }

    IEnumerator InitiatingTexts()
    {
        GameManager.Instance.gamePausable = true;

        yield return null;

        GameManager.Instance.playerCannotMove = false;

        foreach (string currentInfo in info)
        {
            InfoPanel.Instance.AddText(string.Empty, currentInfo, 1f);
        }
    }
}
