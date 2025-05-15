using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject infoImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text infoText;

    float fadeTimer;

    Color generalColor = Color.white;
    Color textColor = Color.black;
    Color bgColor = Color.black;

    List <RelicsInventoryScriptableObject> takenRelics = new List<RelicsInventoryScriptableObject>();

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
        Time.timeScale = 0;
        GameManager.Instance.inInfo = true;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;

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
        bgColor.a = 0.8f;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        GameManager.Instance.infoShowed = true;
        takenRelics.RemoveAt(0);
    }

    public IEnumerator DespawnInfo(float fadeDuration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;

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
        GameManager.Instance.inInfo = false;
        Time.timeScale = 1;
    }

    public void AddRelic(RelicsInventoryScriptableObject nextRelic, float fadeTime)
    {
        takenRelics.Add(nextRelic);
        fadeTimer = fadeTime;

        //Debug.Log(takenRelics.Count);

        if (nextRelic == takenRelics[0])
            ImageTextInfo(nextRelic.description + "\n\n" + nextRelic.effect + nextRelic.value + nextRelic.valueQuantity, nextRelic.image, fadeTime);
    }

    public void ComproveRelicsList()
    {
        GameManager.Instance.infoShowed = false;
        //Debug.Log(takenRelics.Count);

        if (takenRelics.Count == 0)
            StartCoroutine(DespawnInfo(0.75f));
        else
            ImageTextInfo(takenRelics[0].description + "\n\n" + takenRelics[0].effect + takenRelics[0].value + takenRelics[0].valueQuantity, takenRelics[0].image, fadeTimer);
    }
}
