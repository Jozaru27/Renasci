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
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text decorationLine;

    Color generalColor = Color.white;
    Color textColor = Color.white;
    Color bgColor = Color.black;

    List<RelicsInventoryScriptableObject> takenRelics = new List<RelicsInventoryScriptableObject>();
    List<string> infoTexts = new List<string>();
    List<Sprite> infoImages = new List<Sprite>();
    List<bool> infoWithImage = new List<bool>();

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
        nameText.color = textColor;
        decorationLine.color = textColor;
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

        generalColor.a = 0;
        textColor.a = 0;
        bgColor.a = 0;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        nameText.color = textColor;
        decorationLine.color = textColor;

        while (timeElapsed < fadeDuration && fadeDuration != 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            generalColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0, 0.9f, (timeElapsed / fadeDuration));

            infoPanel.GetComponent<Image>().color = generalColor;
            infoImage.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
            infoText.color = textColor;
            nameText.color = textColor;
            decorationLine.color = textColor;

            yield return null;
        }

        generalColor.a = 1;
        textColor.a = 1;
        bgColor.a = 0.9f;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        nameText.color = textColor;
        decorationLine.color = textColor;
        GameManager.Instance.infoShowed = true;

        if (takenRelics.Count > 0)
            takenRelics.RemoveAt(0);

        if (infoTexts.Count > 0)
        {
            infoTexts.RemoveAt(0);
            infoWithImage.RemoveAt(0);
        }
    }

    public IEnumerator DespawnInfo(float fadeDuration)
    {
        float timeElapsed = 0f;

        generalColor.a = 1;
        textColor.a = 1;
        bgColor.a = 0.9f;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        nameText.color = textColor;
        decorationLine.color = textColor;

        while (timeElapsed < fadeDuration && fadeDuration != 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            generalColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0.9f, 0, (timeElapsed / fadeDuration));

            infoPanel.GetComponent<Image>().color = generalColor;
            infoImage.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
            infoText.color = textColor;
            nameText.color = textColor;
            decorationLine.color = textColor;

            yield return null;
        }

        generalColor.a = 0;
        textColor.a = 0;
        bgColor.a = 0;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        nameText.color = textColor;
        decorationLine.color = textColor;

        infoCanvas.SetActive(false);
        GameManager.Instance.inInfo = false;
        Time.timeScale = 1;

        if (!GameManager.Instance.alreadyStarted)
            GameManager.Instance.alreadyStarted = true;
    }

    public void AddRelic(string relName, string relDescription, RelicsInventoryScriptableObject nextRelic, float fadeTime)
    {
        takenRelics.Add(nextRelic);

        if (nextRelic == takenRelics[0])
        {
            if (nextRelic.valueQuantity != 0)
            {
                nameText.text = relName;
                ImageTextInfo(relDescription + " " + nextRelic.value + nextRelic.valueQuantity, nextRelic.image, fadeTime);
            } 
            else
            {
                nameText.text = relName;
                ImageTextInfo(relDescription, nextRelic.image, fadeTime);
            }
        }
    }

    public void AddText(string infoText, float fadeTime)
    {
        infoTexts.Add(infoText);
        infoWithImage.Add(false);

        if (infoText == infoTexts[0])
            TextInfo(infoText, fadeTime);
    }

    public void AddTextWithImage(string infoText, Sprite infoImage, float fadeTime)
    {
        infoTexts.Add(infoText);
        infoImages.Add(infoImage);
        infoWithImage.Add(true);

        if (infoText == infoTexts[0])
            ImageTextInfo(infoText, infoImage, fadeTime);
    }

    public void CheckRelicsList()
    {
        GameManager.Instance.infoShowed = false;

        if (GameManager.Instance.alreadyStarted)
        {
            if (takenRelics.Count == 0)
                StartCoroutine(DespawnInfo(0.75f));
            else
                ImageTextInfo(takenRelics[0].description + "\n\n" + takenRelics[0].effect + takenRelics[0].value + takenRelics[0].valueQuantity, takenRelics[0].image, 0.125f);
        }
        else
        {
            if (infoTexts.Count == 0)
                StartCoroutine(DespawnInfo(0.75f));
            else
            {
                if (infoWithImage[0])
                    ImageTextInfo(infoTexts[0], infoImages[0], 0.125f);
                else
                    TextInfo(infoTexts[0], 0.125f);
            }
        }
    }
}
