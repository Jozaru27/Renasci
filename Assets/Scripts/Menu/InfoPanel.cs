using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject infoImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text infoText;

    Color generalColor = Color.white;
    Color textColor = Color.black;
    Color bgColor = Color.black;

    public static InfoPanel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        generalColor.a = 0;
        textColor.a = 0;
        bgColor.a = 0;
        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
    }

    public void TextInfo(string text, float fadeDuration)
    {
        infoImage.SetActive(false);
        infoCanvas.SetActive(true);
        infoText.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        infoText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 650);
        infoText.text = text;
        StartCoroutine(SpawnInfo(fadeDuration));
    }

    public void ImageTextInfo(string text, Sprite image, float fadeDuration)
    {
        infoImage.SetActive(true);
        infoCanvas.SetActive(true);
        infoImage.gameObject.GetComponent<Image>().sprite = image;
        infoText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(255, 0, 0);
        infoText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 650);
        infoText.text = text;
        StartCoroutine(SpawnInfo(fadeDuration));
    }

    IEnumerator SpawnInfo(float fadeDuration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;

            generalColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0, 0.8f, (timeElapsed / fadeDuration));

            infoPanel.GetComponent<Image>().color = generalColor;
            infoImage.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
            infoText.color = textColor;

            yield return null;
        }

        generalColor.a = 1;
        textColor.a = 1;
        bgColor.a = 1;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        StartCoroutine(DespawnInfo(fadeDuration));
    }

    IEnumerator DespawnInfo(float fadeDuration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;

            generalColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0.8f, 0, (timeElapsed / fadeDuration));

            infoPanel.GetComponent<Image>().color = generalColor;
            infoImage.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
            infoText.color = textColor;

            yield return null;
        }

        generalColor.a = 0;
        textColor.a = 0;
        bgColor.a = 0;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;

        infoCanvas.SetActive(false);
    }
}
