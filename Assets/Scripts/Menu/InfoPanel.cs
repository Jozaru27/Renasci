using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject infoImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text infoText;
    [SerializeField] TMP_Text nameText;

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
        nameText.color = textColor;
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

        while (timeElapsed < fadeDuration && fadeDuration != 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            generalColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(0, 1, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0, 0.8f, (timeElapsed / fadeDuration));

            infoPanel.GetComponent<Image>().color = generalColor;
            infoImage.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
            infoText.color = textColor;
            nameText.color = textColor;

            yield return null;
        }

        generalColor.a = 1;
        textColor.a = 1;
        bgColor.a = 0.8f;

        infoPanel.GetComponent<Image>().color = generalColor;
        infoImage.GetComponent<Image>().color = generalColor;
        backgroundImage.color = bgColor;
        infoText.color = textColor;
        nameText.color = textColor;
        GameManager.Instance.infoShowed = true;
        takenRelics.RemoveAt(0);
    }

    public IEnumerator DespawnInfo(float fadeDuration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration && fadeDuration != 0)
        {
            timeElapsed += Time.unscaledDeltaTime;

            generalColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            textColor.a = Mathf.Lerp(1, 0, (timeElapsed / fadeDuration));
            bgColor.a = Mathf.Lerp(0.8f, 0, (timeElapsed / fadeDuration));

            infoPanel.GetComponent<Image>().color = generalColor;
            infoImage.GetComponent<Image>().color = generalColor;
            backgroundImage.color = bgColor;
            infoText.color = textColor;
            nameText.color = textColor;

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

        infoCanvas.SetActive(false);
        GameManager.Instance.inInfo = false;
        Time.timeScale = 1;
    }

    public void AddRelic(string name, string effect, RelicsInventoryScriptableObject nextRelic, float fadeTime)
    {
        takenRelics.Add(nextRelic);
        fadeTimer = fadeTime;

        //Debug.Log(takenRelics.Count);

        Debug.Log("ABCDEFG");

        //infoText.gameObject.GetComponent<LocalizeStringEvent>().SetEntry("description", new StringVariable { Value = "Aasd"});

        infoText.gameObject.GetComponent<LocalizeStringEvent>().RefreshString();

        if (nextRelic == takenRelics[0])
        {
            if (nextRelic.valueQuantity != 0)
                ImageTextInfo(nextRelic.description + "\n\n" + nextRelic.effect + nextRelic.value + nextRelic.valueQuantity, nextRelic.image, fadeTime);
            else
                ImageTextInfo(nextRelic.description + "\n\n" + nextRelic.effect, nextRelic.image, fadeTime);
        }

        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("description_en", out IVariable descriptionVarible_en))
            ((StringVariable)descriptionVarible_en).Value = nextRelic.description;
        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("description_es", out IVariable descriptionVarible_es))
            ((StringVariable)descriptionVarible_es).Value = nextRelic.description;
        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("description_vlc", out IVariable descriptionVarible_vlc))
            ((StringVariable)descriptionVarible_vlc).Value = nextRelic.description;
        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("effect_en", out IVariable effectVarible_en))
            ((StringVariable)effectVarible_en).Value = nextRelic.effect;
        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("effect_es", out IVariable effectVarible_es))
            ((StringVariable)effectVarible_es).Value = nextRelic.effect;
        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("effect_vlc", out IVariable effectVarible_vlc))
            ((StringVariable)effectVarible_vlc).Value = nextRelic.effect;

        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("value", out IVariable valueVariable))
            ((StringVariable)valueVariable).Value = nextRelic.value;

        if (infoText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("valueQuantity", out IVariable valueQuantityVariable))
            ((StringVariable)valueVariable).Value = nextRelic.valueQuantity.ToString();

        if (nameText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("name_en", out IVariable nameVariable_en))
            ((StringVariable)nameVariable_en).Value = nextRelic.relicName;
        if (nameText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("name_es", out IVariable nameVariable_es))
            ((StringVariable)nameVariable_es).Value = nextRelic.relicName;
        if (nameText.gameObject.GetComponent<LocalizeStringEvent>().StringReference.TryGetValue("name_vlc", out IVariable nameVariable_vlc))
            ((StringVariable)nameVariable_vlc).Value = nextRelic.relicName;
    }

    public void CheckRelicsList()
    {
        GameManager.Instance.infoShowed = false;
        //Debug.Log(takenRelics.Count);

        if (takenRelics.Count == 0)
            StartCoroutine(DespawnInfo(0.75f));
        else
            ImageTextInfo(takenRelics[0].description + "\n\n" + takenRelics[0].effect + takenRelics[0].value + takenRelics[0].valueQuantity, takenRelics[0].image, 0);

        Debug.Log(fadeTimer);
    }
}
