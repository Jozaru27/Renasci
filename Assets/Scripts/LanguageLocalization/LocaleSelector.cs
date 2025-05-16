using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using static GameManager;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;

    public TMP_Dropdown languageDropdown;

    void Start()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        int index = LocalizationSettings.AvailableLocales.Locales.FindIndex(locale => locale == currentLocale);
        languageDropdown.value = index;
        languageDropdown.RefreshShownValue();
    }
        
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        switch (_localeID)
        {
            case 0:
                GameManager.Instance.currentLanguaje = Languajes.ENGLISH;
                break;
            case 1:
                GameManager.Instance.currentLanguaje = Languajes.SPANISH;
                break;
            case 2:
                GameManager.Instance.currentLanguaje = Languajes.VALENCIAN;
                break;
        }

        active = false;
    }
}
