using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject infoImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text infoText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text decorationLine;
    [SerializeField] AudioClip interactSound;
    [SerializeField] TMP_Text[] noteTexts;
    [SerializeField] Image[] noteImages;

    bool firstPanelShown;
    Color generalColor = Color.white;
    Color textColor = Color.white;
    Color bgColor = Color.black;
    AudioSource audioSource;

    //List<RelicsInventoryScriptableObject> takenRelics = new List<RelicsInventoryScriptableObject>();
    List<string> infoTexts = new List<string>();
    List<Sprite> infoImages = new List<Sprite>();
    List<bool> infoWithImage = new List<bool>();
    List<string> infoNames = new List<string>();
    List<bool> haveName = new List<bool>();

    public static InfoPanel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

        if (text == string.Empty)
        {
            infoImage.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            infoImage.GetComponent<RectTransform>().sizeDelta = new Vector2(853 * 1.5f, 480 * 1.5f);
        }
        else
        {
            infoImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-310, 0, 0);
            infoImage.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 350);
        }

        infoText.text = text;
        StartCoroutine(SpawnInfo(fadeDuration));
    }

    IEnumerator SpawnInfo(float fadeDuration)
    {
        if (haveName[0])
            decorationLine.gameObject.SetActive(true);
        else
            decorationLine.gameObject.SetActive(false);

        Time.timeScale = 0f;
        GameManager.Instance.gamePausable = false;
        GameManager.Instance.inInfo = true;
        float timeElapsed = 0f;

        generalColor.a = 0;
        textColor.a = 0;
        bgColor.a = 0;

        if (!firstPanelShown)
        {
            infoPanel.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
        }

        infoImage.GetComponent<Image>().color = generalColor;
        infoText.color = textColor;
        nameText.color = textColor;
        decorationLine.color = textColor;
        
        for (int i = 0; i < noteTexts.Length; i++)
        {
            noteTexts[i].color = textColor;
            noteImages[i].color = textColor;
        }

        while (timeElapsed < fadeDuration && fadeDuration != 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            generalColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0, 0.9f, (timeElapsed / fadeDuration));

            if (!firstPanelShown)
            {
                infoPanel.GetComponent<Image>().color = generalColor;
                backgroundImage.color = bgColor;
            }

            infoImage.GetComponent<Image>().color = generalColor;
            infoText.color = textColor;
            nameText.color = textColor;
            decorationLine.color = textColor;

            for (int i = 0; i < noteTexts.Length; i++)
            {
                noteTexts[i].color = textColor;
                noteImages[i].color = textColor;
            }

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

        for (int i = 0; i < noteTexts.Length; i++)
        {
            noteTexts[i].color = textColor;
            noteImages[i].color = textColor;
        }

        GameManager.Instance.infoShowed = true;

        //if (takenRelics.Count > 0)
        //    takenRelics.RemoveAt(0);

        if (infoTexts.Count > 0)
        {
            infoTexts.RemoveAt(0);

            if (infoWithImage[0])
                infoImages.RemoveAt(0);

            infoWithImage.RemoveAt(0);
            infoNames.RemoveAt(0);
            haveName.RemoveAt(0);
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

        for (int i = 0; i < noteTexts.Length; i++)
        {
            noteTexts[i].color = textColor;
            noteImages[i].color = textColor;
        }

        while (timeElapsed < fadeDuration && fadeDuration != 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            generalColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0.9f, 0, (timeElapsed / fadeDuration));

            if (infoTexts.Count == 0)
            {
                infoPanel.GetComponent<Image>().color = generalColor;
                backgroundImage.color = bgColor;
            }

            infoImage.GetComponent<Image>().color = generalColor;
            infoText.color = textColor;
            nameText.color = textColor;
            decorationLine.color = textColor;

            for (int i = 0; i < noteTexts.Length; i++)
            {
                noteTexts[i].color = textColor;
                noteImages[i].color = textColor;
            }

            yield return null;
        }

        generalColor.a = 0;
        textColor.a = 0;
        bgColor.a = 0;

        if (infoTexts.Count == 0)
        {
            infoPanel.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
        }
        
        infoImage.GetComponent<Image>().color = generalColor;
        infoText.color = textColor;
        nameText.color = textColor;
        decorationLine.color = textColor;

        for (int i = 0; i < noteTexts.Length; i++)
        {
            noteTexts[i].color = textColor;
            noteImages[i].color = textColor;
        }

        CheckList();

        //if (!GameManager.Instance.alreadyStarted)
        //    GameManager.Instance.alreadyStarted = true;
    }

    //public void AddRelic(string relName, string relDescription, RelicsInventoryScriptableObject nextRelic, float fadeTime)
    //{
    //    takenRelics.Add(nextRelic);

    //    if (nextRelic == takenRelics[0])
    //    {
    //        if (nextRelic.valueQuantity != 0)
    //        {
    //            nameText.text = relName;
    //            ImageTextInfo(relDescription + " " + nextRelic.value + nextRelic.valueQuantity, nextRelic.image, fadeTime);
    //        } 
    //        else
    //        {
    //            nameText.text = relName;
    //            ImageTextInfo(relDescription, nextRelic.image, fadeTime);
    //        }
    //    }
    //}

    public void AddText(string textName, string infoText, float fadeTime)
    {
        infoTexts.Add(infoText);
        infoWithImage.Add(false);
        infoNames.Add(textName);
        
        if (textName == string.Empty)
            haveName.Add(false);
        else
            haveName.Add(true);

        if (infoText == infoTexts[0])
        {
            nameText.text = textName;
            TextInfo(infoText, fadeTime);
        }
    }

    public void AddTextWithImage(string textName, string infoText, Sprite infoImage, float fadeTime)
    {
        infoTexts.Add(infoText);
        infoImages.Add(infoImage);
        infoWithImage.Add(true);
        infoNames.Add(textName);
        
        if (textName == string.Empty)
            haveName.Add(false);
        else
            haveName.Add(true);

        if (infoText == infoTexts[0])
        {
            nameText.text = textName;
            ImageTextInfo(infoText, infoImage, fadeTime);
        }   
    }

    public void ConfirmFade()
    {
        GameManager.Instance.infoShowed = false;
        StartCoroutine(DespawnInfo(0.75f));

        audioSource.PlayOneShot(interactSound, 2f);

        firstPanelShown = true;

        //if (infoTexts.Count == 0)
        //    StartCoroutine(DespawnInfo(0.75f));
        //else
        //{
        //    if (infoWithImage[0])
        //        ImageTextInfo(infoTexts[0], infoImages[0], 1);
        //    else
        //        TextInfo(infoTexts[0], 1);

        //    nameText.text = infoNames[0];
        //}
    }

    void CheckList()
    {
        if (infoTexts.Count == 0)
        {
            infoCanvas.SetActive(false);
            GameManager.Instance.inInfo = false;
            Time.timeScale = 1;
            firstPanelShown = false;
            GameManager.Instance.gamePausable = true;
            ControlManager.Instance.affectInfoImage = false;
        }
        else
        {
            if (infoWithImage[0])
                ImageTextInfo(infoTexts[0], infoImages[0], 1);
            else
                TextInfo(infoTexts[0], 1);

            nameText.text = infoNames[0];
        }
    }
}
