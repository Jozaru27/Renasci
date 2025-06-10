using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FirstInfo : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    [SerializeField] string[] textNames;
    [TextArea(4, 6)][SerializeField] string[] info;

    [SerializeField] Image fadeImg;

    private void Start()
    {
        StartCoroutine(InitialFade());
    }

    IEnumerator InitialFade()
    {
        Time.timeScale = 0f;

        Color imageColor = Color.black;
        imageColor.a = 1f;
        fadeImg.color = imageColor;

        while (fadeImg.color.a > 0)
        {
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

        InfoPanel.Instance.AddText(textNames[0], info[0], 1f);
        InfoPanel.Instance.AddTextWithImage(string.Empty, string.Empty, ControlManager.Instance.fullCurrentControls, 1f);
    }

    public void ChangeFirstSentenceName(string newName)
    {
        textNames[0] = newName;
    }

    public void ChangeFirstSentenceDescription(string newDesc)
    {
        info[0] = newDesc;
    }

    public void ChangeSecondSentenceName(string newName)
    {
        textNames[1] = newName;
    }

    public void ChangeSecondSentenceDescription(string newDesc)
    {
        info[1] = newDesc;
    }
}
