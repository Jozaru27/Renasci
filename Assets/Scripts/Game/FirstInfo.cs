using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FirstInfo : MonoBehaviour
{
    [SerializeField] string[] textNames;
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

        for (int i = 0; i < info.Length; i++)
        {
            InfoPanel.Instance.AddText(textNames[i], info[i], 1f);
        }
    }
}
