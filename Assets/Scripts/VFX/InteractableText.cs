using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableText : MonoBehaviour
{
    [SerializeField] Image interactImage;
    [SerializeField] TMP_Text interactText;

    public void Holding()
    {
        StartCoroutine(HoldingDelay());
    }

    IEnumerator HoldingDelay()
    {
        yield return null;

        Hold();
    }

    void Hold()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnInteractUI());
    }

    public void Unholding()
    {
        StopAllCoroutines();
        StartCoroutine(DespawnInteractUI());
    }

    IEnumerator SpawnInteractUI()
    {
        Color fadeColor = Color.white;
        fadeColor.a = interactImage.color.a;
        interactImage.color = fadeColor;
        interactText.color = fadeColor;

        while (interactImage.color.a < 1)
        {
            fadeColor.a += 2 * Time.unscaledDeltaTime;
            interactImage.color = fadeColor;
            interactText.color = fadeColor;

            yield return null;
        }
    }

    IEnumerator DespawnInteractUI()
    {
        Color fadeColor = Color.white;
        fadeColor.a = interactImage.color.a;
        interactImage.color = fadeColor;
        interactText.color = fadeColor;

        while (interactImage.color.a > 0)
        {
            fadeColor.a -= 2 * Time.unscaledDeltaTime;
            interactImage.color = fadeColor;
            interactText.color = fadeColor;

            yield return null;
        }
    }
}
