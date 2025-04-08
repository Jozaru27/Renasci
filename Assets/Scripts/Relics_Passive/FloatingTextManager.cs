using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public Canvas canvas;
    public float floatSpeed = 1f;
    public float fadeDuration = 2f;

    public void ShowFloatingText(string text/*, Vector3 position*/)
    {
        //if (canvas == null)
        //    return;

        //GameObject floatingText = Instantiate(floatingTextPrefab, canvas.transform);
        //floatingText.transform.position = Camera.main.WorldToScreenPoint(position);

        //Text textComponent = floatingText.GetComponent<Text>();

        TMP_Text textComponent = UIManager.Instance.relicsText;

        if (textComponent != null)
        {
            textComponent.text = text;
            //StartCoroutine(FloatAndFade(floatingText, textComponent));
            StartCoroutine(FloatAndFade(UIManager.Instance.relicsUI, textComponent));
        }
    }

    private IEnumerator FloatAndFade(GameObject floatingText, /*Text*/ TMP_Text textComponent)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = floatingText.transform.position;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            floatingText.transform.position = startPosition + Vector3.up * floatSpeed * elapsedTime;

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color color = textComponent.color;
            color.a = alpha;
            textComponent.color = color;

            yield return null;
        }

        textComponent.text = string.Empty;
        //Destroy(floatingText);
    }
}